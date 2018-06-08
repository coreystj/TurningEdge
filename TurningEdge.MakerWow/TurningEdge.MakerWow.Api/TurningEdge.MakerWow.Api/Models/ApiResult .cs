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
    public class ApiResult<T> : ApiContext
        where T : JsonObject
    {
        public override ApiException Error
        {
            get
            {
                try
                {
                    return _json.Json["error"].ToError();
                }
                catch (Exception e)
                {
                    var error = new ApiException(1, "Json Parse Error: " + _json.RawJson, e);
                    return error;
                }
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

        public ApiResult(JsonResult json)
            : base(json)
        {
        }

        public ApiResult(string rawJson)
            : base(rawJson)
        {
        }

    }
}
