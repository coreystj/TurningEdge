using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Helpers;
using TurningEdge.MakerWow.Models;
using TurningEdge.Web.Models;

namespace TurningEdge.MakerWow.Api.Models.Abstracts
{
    public abstract class ApiContext
    {
        protected JsonResult _json;

        public bool IsError
        {
            get
            {
                return (Error == null || Error.Id != 0);
            }
        }

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
                return _json.Json["user"].ParseSingle<UserJsonObject>();
            }
        }

        public abstract ApiException Error
        {
            get;
        }

        public string Sql
        {
            get
            {
                return (string)_json.Json["sql"];
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

        public ApiContext(JsonResult json)
        {
            _json = json;
        }

        public ApiContext(string rawJson)
        {
            _json = new JsonResult(rawJson);
        }

    }
}
