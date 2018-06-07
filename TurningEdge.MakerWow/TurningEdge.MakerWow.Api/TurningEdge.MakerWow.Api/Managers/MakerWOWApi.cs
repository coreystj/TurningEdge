using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.DataTypes;
using TurningEdge.MakerWow.Api.Delegates;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Helpers;
using TurningEdge.MakerWow.Api.Models;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.Web.Exceptions;
using TurningEdge.Web.WebContext.Delegates;
using TurningEdge.Web.WebContext.Interfaces;
using TurningEdge.Web.WebResult.Interfaces;

namespace TurningEdge.MakerWow.Api.Managers
{
    public static class MakerWOWApi
    {
        private static User _user;
        private const string _baseUrl = "http://localhost:8080/api.php?action=";
        private static IWebContext _webContext;

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

            DoPostRequest(formData, _baseUrl + "account&process=login",
            (IWebRequest result) => {
                var apiResult = new ApiResult<ChunkData>(result.Json);
                var error = apiResult.Error;
                if (error.Id > 0)
                    onLoginFailed(error);
                else
                {
                    var user = apiResult.CurrentUser;
                    _user = user;
                    onLoginSuccess(_user);
                }
            },
            (WebContextException error) => {
                onLoginFailed(new ApiException(1, "Could not login.", error));
            });
        }

        public static void Logout(OnLogoutSuccessAction onLogoutSuccess, OnLogoutFailedAction onLogoutFailed)
        {
            var formData = new Dictionary<string, string>();

            DoPostRequest(formData, _baseUrl + "account&process=logout",
            (IWebRequest result) => {
                var apiResult = new ApiResult<ChunkData>(result.Json);
                var error = apiResult.Error;
                if (error.Id > 0)
                    onLogoutFailed(error);
                else
                {
                    var user = apiResult.CurrentUser;
                    _user = null;
                    onLogoutSuccess();
                }
            },
            (WebContextException error) => {
                onLogoutFailed(new ApiException(1, "Could not login, check error logs.", error));
            });
        }

        public static void GetChunkData(
            OnGetChunkDataSuccessAction onGetWorldDataSuccess, 
            OnGetChunkDataFailedAction onGetWorldDataFailed)
        {
            DoGetRequest(_baseUrl + "read&table=chunk_data&id=" + Id,
            (IWebRequest result) => {
                var apiResult = new ApiResult<ChunkData>(result.Json);
                var error = apiResult.Error;
                if (error.Id > 0)
                    onGetWorldDataFailed(new ApiException(1, "Could not retrieve chunk data.", error));
                else
                {
                    var user = apiResult.CurrentUser;
                    _user = null;
                    onGetWorldDataSuccess(apiResult.Records);
                }
            },
            (WebContextException error) => {
                onGetWorldDataFailed(new ApiException(1, "Could not retrieve chunk data.", error));
            });
        }

        public static void SetChunkData(
            ChunkData[] chunkDatas,
            OnSetChunkDataSuccessAction onSetChunkDataSuccess,
            OnSetChunkDataFailedAction onSetChunkDataFailed)
        {
            var formData = new Dictionary<string, string>();
            formData.Add("records", chunkDatas.SerializeJson());

            DoPostRequest(formData,
            _baseUrl + "create&table=chunk_data&id=" + Id,
            (IWebRequest result) => {
                onSetChunkDataSuccess();
            },
            (WebContextException error) => {
                onSetChunkDataFailed(new ApiException(1, "Could not set chunk data.", error));
            });
        }

        public static void GetWorldLayers(
            OnGetWorldLayersSuccessAction onGetWorldDataSuccess,
            OnGetWorldLayersFailedAction onGetWorldDataFailed)
        {
            DoGetRequest(_baseUrl + "read&table=world_layers&id=" + Id,
            (IWebRequest result) => {
                var apiResult = new ApiResult<WorldLayer>(result.Json);
                var error = apiResult.Error;
                if (error.Id > 0)
                    onGetWorldDataFailed(new ApiException(1, "Could not retrieve world layer.", error));
                else
                {
                    var user = apiResult.CurrentUser;
                    onGetWorldDataSuccess(apiResult.Records);
                }
            },
            (WebContextException error) => {
                onGetWorldDataFailed(new ApiException(1, "Could not retrieve world layer.", error));
            });
        }

        public static void SetWorldLayers(
            WorldLayer[] worldLayers,
            OnSetWorldLayersSuccessAction onSetWorldLayersSuccess,
            OnSetWorldLayersFailedAction onSetWorldLayersFailed)
        {
            var formData = new Dictionary<string, string>();
            formData.Add("records", worldLayers.SerializeJson());

            DoPostRequest(formData,
            _baseUrl + "read&table=world_layers&id=" + Id,
            (IWebRequest result) => {
                onSetWorldLayersSuccess();
            },
            (WebContextException error) => {
                onSetWorldLayersFailed(new ApiException(1, "Could not set world layer.", error));
            });
        }

        private static void DoGetRequest(string url, 
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
                url,
                onSuccessAction,
                onFailedAction);
        }

        private static void DoPostRequest(
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
                url,
                onSuccessAction,
                onFailedAction);
        }
    }
}
