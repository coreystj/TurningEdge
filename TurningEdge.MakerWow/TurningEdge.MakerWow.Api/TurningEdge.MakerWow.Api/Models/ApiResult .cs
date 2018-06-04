using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Helpers;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.Web.Models;

namespace TurningEdge.MakerWow.Api.Models
{
    public class ApiResult<T>
        where T : JsonObject
    {
        private JsonResult _json;

        public string Url
        {
            get
            {
                return (string)_json.Json["url"];
            }
        }

        public string DatetimeFormat
        {
            get
            {
                return (string)_json.Json["datetime_format"];
            }
        }

        public DateTime Datetime
        {
            get
            {
                return DateTime.ParseExact(
                    (string)_json.Json["datetime"], 
                    "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
            }
        }

        public Dictionary<string, object> GetArguments
        {
            get
            {
                var colection = (ICollection)_json.Json["GET"];
                if (colection.Count > 0)
                    return (Dictionary<string, object>)colection;
                else
                    return new Dictionary<string, object>();
            }
        }

        public Dictionary<string, object> PostArguments
        {
            get
            {
                var colection = (ICollection)_json.Json["POST"];
                if (colection.Count > 0)
                    return (Dictionary<string, object>)colection;
                else
                    return new Dictionary<string, object>();
            }
        }

        public User CurrentUser
        {
            get
            {
                return _json.Json["user"].ParseSingle<User>();
            }
        }

        public ApiException Error
        {
            get
            {
                return _json.Json["error"].ToError();
            }
        }

        public string Sql
        {
            get
            {
                return (string)_json.Json["sql"];
            }
        }

        public T[] Records
        {
            get
            {
                return ((Dictionary<string, object>)_json.Json["result"])
                    ["records"].Parse<T>();
            }
        }

        public bool IsWritingLogs
        {
            get
            {
                return (bool)_json.Json["write_logs"];
            }
        }

        public JsonResult Json
        {
            get { return _json; }
        }

        public ApiResult(JsonResult json)
        {
            _json = json;
        }

        public ApiResult(string rawJson)
        {
            _json = new JsonResult(rawJson);
        }

    }
}
