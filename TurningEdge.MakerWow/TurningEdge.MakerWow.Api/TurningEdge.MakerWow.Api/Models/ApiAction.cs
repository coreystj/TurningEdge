using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Helpers;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.Web.Models;

namespace TurningEdge.MakerWow.Api.Models
{
    public class ApiAction : ApiContext
    {
        public override ApiException Error
        {
            get
            {
                string errorString = (string)((Dictionary<string, object>)
                    _json.Json["result"])["error"];

                if (!string.IsNullOrEmpty((string)((Dictionary<string, object>)_json.Json["error"])["message"]))
                    return _json.Json["error"].ToError();
                else if (!string.IsNullOrEmpty(errorString))
                    return new ApiException(2, errorString);
                else
                    return new ApiException(0, string.Empty);
            }
        }

        public int RowsAffected
        {
            get
            {
                string affectedRowsString = (string)(((Dictionary<string, object>)_json.Json["result"])
                    ["affected_rows"].ToString());
                if (!string.IsNullOrEmpty(affectedRowsString))
                {
                    return int.Parse(affectedRowsString);
                }
                else
                    return -1;
            }
        }

        public int LastRowAffected
        {
            get
            {
                string lastIdString = (string)(((Dictionary<string, object>)_json.Json["result"])
                    ["last_id"].ToString());
                if (!string.IsNullOrEmpty(lastIdString))
                {
                    return int.Parse(lastIdString);
                }
                else
                    return -1;
            }
        }

        public ApiAction(JsonResult json)
            : base(json)
        {
        }

        public ApiAction(string rawJson)
            : base(rawJson)
        {
        }

    }
}
