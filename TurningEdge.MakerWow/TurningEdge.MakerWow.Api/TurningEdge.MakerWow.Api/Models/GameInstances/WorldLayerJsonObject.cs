using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.Interfaces;
using TurningEdge.MakerWow.Models.GameInstances;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    public class WorldLayerJsonObject : WorldLayer, IJsonObject
    {
        public WorldLayerJsonObject(object record) 
        {
            ParseJson(record as Dictionary<string, object>);
        }

        public void ParseJson(Dictionary<string, object> record)
        {
            _id = int.Parse((string)record["id"]);
            _userId = int.Parse((string)record["user_id"]);
            _name = (string)record["name"];
            _description = (string)record["description"];
            _environmentId = int.Parse((string)record["environment"]);
        }

        public string SerializeJson()
        {
            var record = new StringBuilder();
            record.Append("{");

            record.Append("\"" + "id" + "\"" + " : " + "\"" + _id + "\"" + ",");
            record.Append("\"" + "user_id" + "\"" + " : " + "\"" + _userId + "\"" + ",");
            record.Append("\"" + "name" + "\"" + " : " + "\"" + _name + "\"" + ",");
            record.Append("\"" + "description" + "\"" + " : " + "\"" + _description + "\"" + ",");
            record.Append("\"" + "environment" + "\"" + " : " + "\"" + _environmentId + "\"");

            record.Append("}");

            return record.ToString();
        }
    }
}
