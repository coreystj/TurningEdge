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

        public static void GetChunkData(
            OnGetChunkDataSuccessAction onGetWorldDataSuccess, 
            OnGetChunkDataFailedAction onGetWorldDataFailed)
        {
            DoGetRequest(_baseUrl + "read&table=chunk_data&id=61",
            (IWebRequest result) => {
                var apiResult = new ApiResult<ChunkData>(result.Json);
                var user = apiResult.CurrentUser;
                var error = apiResult.Error;
                onGetWorldDataSuccess(apiResult.Records);
            },
            (WebContextException error) => {
                onGetWorldDataFailed(new ApiException(0, "Could not retrieve chunk data.", error));
            });
        }

        public static void SetChunkData(
            ChunkData[] chunkDatas,
            OnSetChunkDataSuccessAction onSetChunkDataSuccess,
            OnSetChunkDataFailedAction onSetChunkDataFailed)
        {
            var formData = new Dictionary<string, string>();
            formData.Add("records", JSON.JSONWriter.ToJson(chunkDatas));

            DoPostRequest(formData,
            _baseUrl + "read&table=chunk_data&id=61",
            (IWebRequest result) => {
                onSetChunkDataSuccess();
            },
            (WebContextException error) => {
                onSetChunkDataFailed(new ApiException(0, "Could not set chunk data.", error));
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
