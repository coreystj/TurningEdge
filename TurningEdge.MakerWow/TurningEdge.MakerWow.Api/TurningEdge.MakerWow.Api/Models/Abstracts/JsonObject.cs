using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Api.Models.Abstracts
{
    public abstract class JsonObject
    {
        public JsonObject(object record)
        {
            ParseJson(record);
        }

        protected abstract void ParseJson(object record);
    }
}
