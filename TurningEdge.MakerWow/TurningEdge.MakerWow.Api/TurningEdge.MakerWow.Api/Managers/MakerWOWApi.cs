using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private const string _baseUrl = "http://localhost:8080/api.php?action=";
        private static IWebContext _webContext;

        static MakerWOWApi()
        {

        }

        public static void SetWebContext(IWebContext webContext)
        {
            _webContext = webContext;
        }

        public static void GetWorldData(
            OnGetWorldDataSuccessAction onGetWorldDataSuccess, 
            OnGetWorldDataFailedAction onGetWorldDataFailed)
        {
            DoGetRequest(_baseUrl + "read&table=world_data&id=61",
            (IWebRequest result) => {
                var apiResult = new ApiResult<WorldData>(result.Json);
                var user = apiResult.CurrentUser;
                onGetWorldDataSuccess(apiResult.Records);
            },
            (WebContextException error) => {
                onGetWorldDataFailed(new ApiException("Could not retrieve world data.", error));
            });
        }

        public static void SetWorldData(
            OnSetWorldDataSuccessAction onSetWorldDataSuccess,
            OnSetWorldDataFailedAction onSetWorldDataFailed)
        {
            DoPostRequest("https://www.google.ca/",
            (IWebRequest result) => {
                onSetWorldDataSuccess();
            },
            (WebContextException error) => {
                onSetWorldDataFailed(new ApiException("Could not set world data.", error));
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

        private static void DoPostRequest(string url,
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
                url,
                onSuccessAction,
                onFailedAction);
        }
    }
}
