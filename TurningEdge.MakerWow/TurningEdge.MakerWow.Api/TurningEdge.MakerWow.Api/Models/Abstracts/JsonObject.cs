using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Api.Models.Abstracts
{
    public abstract class JsonObject
    {
        public JsonObject()
        {
        }

        public JsonObject(object record)
        {
            ParseJson(record as Dictionary<string, object>);
        }

        protected abstract void ParseJson(Dictionary<string, object> record);
        public abstract Dictionary<string, object> SerializeJson();
    }
}
