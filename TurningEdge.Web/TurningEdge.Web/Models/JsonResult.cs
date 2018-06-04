using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.Web.Models
{
    public class JsonResult
    {
        private string _rawJson;
        private Dictionary<string, object> _json;

        public string RawJson
        {
            get { return _rawJson; }
        }

        public Dictionary<string, object> Json
        {
            get {
                return _json;
            }
        }


        public JsonResult(string rawJson)
        {
            _rawJson = rawJson;
            _json = JSON.JSONParser.FromJson<Dictionary<string, object>>(_rawJson);
        }
    }
}
