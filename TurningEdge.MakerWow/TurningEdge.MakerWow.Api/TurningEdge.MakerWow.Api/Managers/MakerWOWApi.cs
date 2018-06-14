using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.MakerWow.DataTypes;
using TurningEdge.MakerWow.Api.Delegates;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Helpers;
using TurningEdge.MakerWow.Api.Models;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Api.Repositories;
using TurningEdge.MakerWow.Api.Repositories.Abstracts;
using TurningEdge.MakerWow.Models;
using TurningEdge.Web.Exceptions;
using TurningEdge.Web.WebContext.Delegates;
using TurningEdge.Web.WebContext.Interfaces;
using TurningEdge.Web.WebResult.Interfaces;
using TurningEdge.MakerWow.Models.GameInstances;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.MakerWow.Api.Models.Abstracts;

namespace TurningEdge.MakerWow.Api.Managers
{
    public static class MakerWOWApi
    {
        public static event OnErrorAction OnError = delegate { };

        private static User _user;
        private const string _baseUrl = "http://localhost:8080/api.php?";
        private static IWebContext _webContext;

        private static ChunkDataRepository _chunkDataRepository;
        private static WorldLayerRepository _worldLayerRepository;
        private static InventoryRepository _inventoryRepository;
        private static StockpileRepository _stockpileRepository;
        private static RelationshipSkillUserRepository _relationshipSkillUserRepository;

        private static string _sessionId;

        public static ChunkDataRepository ChunkDataRepository
        {
            get { return _chunkDataRepository; }
        }
        public static WorldLayerRepository WorldLayerRepository
        {
            get { return _worldLayerRepository; }
        }
        public static InventoryRepository InventoryRepository
        {
            get { return _inventoryRepository; }
        }
        public static StockpileRepository StockpileRepository
        {
            get { return _stockpileRepository; }
        }
        public static RelationshipSkillUserRepository RelationshipSkillUserRepository
        {
            get { return _relationshipSkillUserRepository; }
        }

        public static RegistrationStatus Status
        {
            get
            {
                return _user.Status;
            }
        }

        public static DateTime RegisterDate
        {
            get
            {
                return _user.RegisterDate;
            }
        }

        public static string LastName
        {
            get
            {
                return _user.LName;
            }
        }

        public static string FirstName
        {
            get
            {
                return _user.FName;
            }
        }

        public static string Email
        {
            get
            {
                return _user.Email;
            }
        }

        public static int Id
        {
            get
            {
                return _user.Id;
            }
        }

        public static User CurrentUser
        {
            get
            {
                return _user;
            }
        }

        static MakerWOWApi()
        {
            _chunkDataRepository = new ChunkDataRepository();
            _worldLayerRepository = new WorldLayerRepository();
            _inventoryRepository = new InventoryRepository();
            _stockpileRepository = new StockpileRepository();
            _relationshipSkillUserRepository = new RelationshipSkillUserRepository();
        }

        public static void Initialize<T>()
            where T : IWebContext
        {
            var webContextFactory = new Factory<T>();
            IWebContext webContext = webContextFactory.Create<T>();
            MakerWOWApi.SetWebContext(webContext);
        }

        public static void FireOnError(ApiException exception)
        {
            OnError(exception);
        }

        public static void SetWebContext(IWebContext webContext)
        {
            _webContext = webContext;
        }

        public static void Login(SessionCommand sessionCommand, string username, string password, 
            OnLoginSuccessAction onLoginSuccess, OnLoginFailedAction onLoginFailed)
        {
            var formData = new Dictionary<string, string>();
            formData.Add("username", username);
            formData.Add("password", password);

            DoPostRequest(formData, "account&process=login",
            (IWebRequest result) => {
                var apiResult = new ApiResult<ChunkDataJsonObject>(result.Json);
                if (apiResult.IsError)
                    onLoginFailed(sessionCommand, apiResult);
                else
                {
                    var user = apiResult.CurrentUser;
                    _sessionId = apiResult.SessionId;
                    _user = user;
                    onLoginSuccess(sessionCommand, _user, apiResult);
                }
            },
            (WebContextException error) => {
                OnError(new ApiException(1, "Could not login.", error));
            });
        }

        public static void Login(string username, string password,
            OnLoginSuccessAction onLoginSuccess, OnLoginFailedAction onLoginFailed)
        {
            Login(null, username, password, onLoginSuccess, onLoginFailed);
        }

        public static void Logout(SessionCommand sessionCommand, 
            OnLogoutSuccessAction onLogoutSuccess, OnLogoutFailedAction onLogoutFailed)
        {
            var formData = new Dictionary<string, string>();

            DoPostRequest(formData, "account&process=logout",
            (IWebRequest result) => {
                var apiResult = new ApiResult<ChunkDataJsonObject>(result.Json);
                if (apiResult.IsError)
                    onLogoutFailed(sessionCommand, apiResult);
                else
                {
                    _user = null;
                    onLogoutSuccess(sessionCommand, apiResult);
                }
            },
            (WebContextException error) => {
                OnError(new ApiException(1, "Could not login, check error logs.", error));
            });
        }

        public static void DoGetRequest(string url, 
            OnWebRequestSuccessAction successAction, 
            OnWebRequestFailedAction failedAction)
        {
            OnWebRequestSuccessAction onSuccessAction = (IWebRequest result) =>
            {
                successAction(result);
            };

            OnWebRequestFailedAction onFailedAction = (WebContextException error) =>
            {
                failedAction(error);
            };

            _webContext.Get(
                _baseUrl + ((!string.IsNullOrEmpty(_sessionId))?"session_id="
                + _sessionId + "&" : string.Empty) + "action=" + url,
                onSuccessAction,
                onFailedAction);
        }

        public static void DoPostRequest(
            Dictionary<string, string> formData,
            string url,
            OnWebRequestSuccessAction successAction,
            OnWebRequestFailedAction failedAction)
        {
            OnWebRequestSuccessAction onSuccessAction = (IWebRequest result) =>
            {
                successAction(result);
            };

            OnWebRequestFailedAction onFailedAction = (WebContextException error) =>
            {
                failedAction(error);
            };

            _webContext.Post(
                formData,
                _baseUrl + ((!string.IsNullOrEmpty(_sessionId)) ? "session_id=" 
                + _sessionId + "&" : string.Empty) + "action=" + url,
                onSuccessAction,
                onFailedAction);
        }

    }
}
