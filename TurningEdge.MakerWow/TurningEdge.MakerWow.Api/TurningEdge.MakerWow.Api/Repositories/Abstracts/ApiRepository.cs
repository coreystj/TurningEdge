using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Generics.Interfaces;
using TurningEdge.Helpers;
using TurningEdge.MakerWow.Api.DataTypes;
using TurningEdge.MakerWow.Api.Delegates;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Helpers;
using TurningEdge.MakerWow.Api.Interfaces;
using TurningEdge.MakerWow.Api.Managers;
using TurningEdge.MakerWow.Api.Models;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.Web.Exceptions;
using TurningEdge.Web.Helpers;
using TurningEdge.Web.WebResult.Interfaces;

namespace TurningEdge.MakerWow.Api.Repositories.Abstracts
{
    public abstract class ApiRepository<T> : IApiRepository<T>
        where T : JsonObject
    {
        protected HashSet<T> _models;
        protected string _tableName;
        protected string[] _primaryKeys;

        public string TableName
        {
            get { return _tableName; }
        }

        public string[] PrimaryKeys
        {
            get { return _primaryKeys; }
        }

        public T[] Models
        {
            get
            {
                return _models.GetArray();
            }
        }

        public ApiRepository()
        {
            _models = new HashSet<T>();
            var primaryKeys = new List<string>();
            _tableName = SetPrimaryData(primaryKeys);
            _primaryKeys = primaryKeys.ToArray();
        }

        protected abstract string SetPrimaryData(List<string> primaryKeys);

        public void Create(T[] models, OnSuccessAction onCreateSuccessAction, 
            OnFailedAction onCreateFailedAction)
        {
            var formData = new Dictionary<string, string>();
            formData.Add("records", models.SerializeJson());
            formData.Add("primary_keys", JSON.JSONWriter.ToJson(_primaryKeys));

            MakerWOWApi.DoPostRequest(formData, CrudType.Create.ToString().ToLower()
                + "&table="+ _tableName,
            (IWebRequest result) => {
                var apiResult = new ApiAction(result.Json);
                var here = apiResult.LastRowAffected;
                if (apiResult.IsError)
                {
                    onCreateFailedAction(apiResult);
                }
                else
                    onCreateSuccessAction(apiResult);
            },
            (WebContextException error) => {
                MakerWOWApi.FireOnError(new ApiException(1, "Could not create models.", error));
            });
        }

        public void Delete(T[] models, OnSuccessAction onDeleteSuccessAction, 
            OnFailedAction onDeleteFailedAction)
        {
            var formData = new Dictionary<string, string>();
            formData.Add("records", models.SerializeJson());
            formData.Add("primary_keys", JSON.JSONWriter.ToJson(_primaryKeys));

            MakerWOWApi.DoPostRequest(formData, CrudType.Delete.ToString().ToLower()
                + "&table=" + _tableName,
            (IWebRequest result) => {
                var apiResult = new ApiAction(result.Json);
                if (apiResult.IsError)
                    onDeleteFailedAction(apiResult);
                else
                {
                    _models.Remove(models);
                    onDeleteSuccessAction(apiResult);
                }
            },
            (WebContextException error) => {
                MakerWOWApi.FireOnError(new ApiException(1, "Could not update models.", error));
            });
        }

        public void Read(OnGetSuccessAction<T> onReadSuccessAction, 
            OnFailedAction onReadFailedAction, Dictionary<string, string> filter = null)
        {
            MakerWOWApi.DoGetRequest(CrudType.Read.ToString().ToLower() + "&"
                + ((filter != null) ? filter.FormSerialize() : string.Empty) + "&table=" + _tableName,
            (IWebRequest result) => {
                var apiResult = new ApiResult<T>(result.Json);
                if (apiResult.IsError)
                    onReadFailedAction(apiResult);
                else
                {
                    _models.Combine(apiResult.Records);
                    onReadSuccessAction(apiResult.Records, apiResult);
                }
            },
            (WebContextException error) => {
                MakerWOWApi.FireOnError(new ApiException(1, "Could not read models.", error));
            });
        }

        public void Update(T[] models, OnSuccessAction onUpdateSuccessAction, 
            OnFailedAction onUpdateFailedAction)
        {
            var formData = new Dictionary<string, string>();
            formData.Add("records", models.SerializeJson());
            formData.Add("primary_keys", JSON.JSONWriter.ToJson(_primaryKeys));

            MakerWOWApi.DoPostRequest(formData, CrudType.Update.ToString().ToLower()
                + "&table=" + _tableName,
            (IWebRequest result) => {
                var apiResult = new ApiAction(result.Json);

                if (apiResult.IsError)
                    onUpdateFailedAction(apiResult);
                else
                    onUpdateSuccessAction(apiResult);
            },
            (WebContextException error) => {
                MakerWOWApi.FireOnError(new ApiException(1, "Could not update models.", error));
            });
        }
    }
}
