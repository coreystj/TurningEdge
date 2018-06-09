using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.MakerWow.Api.DataTypes;
using TurningEdge.MakerWow.Api.Delegates;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Helpers;
using TurningEdge.MakerWow.Api.Models;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Api.Repositories;
using TurningEdge.MakerWow.Api.Repositories.Abstracts;
using TurningEdge.Web.Exceptions;
using TurningEdge.Web.WebContext.Delegates;
using TurningEdge.Web.WebContext.Interfaces;
using TurningEdge.Web.WebResult.Interfaces;

namespace TurningEdge.MakerWow.Api.Managers
{
    public static class MakerWOWApi
    {
        public static event OnErrorAction OnError = delegate { };

        private static User _user;
        private const string _baseUrl = "http://localhost:8080/api.php?action=";
        private static IWebContext _webContext;

        private static ChunkDataRepository _chunkDataRepository;
        private static WorldLayerRepository _worldLayerRepository;

        public static ChunkDataRepository ChunkDataRepository
        {
            get { return _chunkDataRepository; }
        }
        public static WorldLayerRepository WorldLayerRepository
        {
            get { return _worldLayerRepository; }
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

        public static void Login(string username, string password, 
            OnLoginSuccessAction onLoginSuccess, OnLoginFailedAction onLoginFailed)
        {
            var formData = new Dictionary<string, string>();
            formData.Add("username", username);
            formData.Add("password", password);

            DoPostRequest(formData, "account&process=login",
            (IWebRequest result) => {
                var apiResult = new ApiResult<ChunkData>(result.Json);
                if (apiResult.IsError)
                    onLoginFailed(apiResult);
                else
                {
                    var user = apiResult.CurrentUser;
                    _user = user;
                    onLoginSuccess(_user, apiResult);
                }
            },
            (WebContextException error) => {
                OnError(new ApiException(1, "Could not login.", error));
            });
        }

        public static void Logout(OnLogoutSuccessAction onLogoutSuccess, OnLogoutFailedAction onLogoutFailed)
        {
            var formData = new Dictionary<string, string>();

            DoPostRequest(formData, "account&process=logout",
            (IWebRequest result) => {
                var apiResult = new ApiResult<ChunkData>(result.Json);
                if (apiResult.IsError)
                    onLogoutFailed(apiResult);
                else
                {
                    _user = null;
                    onLogoutSuccess(apiResult);
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
                _baseUrl + url,
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
                _baseUrl + url,
                onSuccessAction,
                onFailedAction);
        }
    }
}
