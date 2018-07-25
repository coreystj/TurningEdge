using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.Interfaces;
using TurningEdge.MakerWow.Models.GameInstances;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    [Serializable]
    public class GroundJsonObject : Ground, IJsonObject
    {
        public GroundJsonObject(int id, string name, string description, string icon)
            : base(id, name, description, icon)
        {
        }

        public GroundJsonObject(object record)
        {
            ParseJson(record as Dictionary<string, object>);
        }

        public void ParseJson(Dictionary<string, object> record)
        {
            _id = int.Parse((string)record["id"]);
            _name = (string)record["name"];
            _description = (string)record["description"];
            _icon = (string)record["icon"];
        }

        public string SerializeJson()
        {
            var record = new StringBuilder();
            record.Append("{");

            record.Append("\"" + "id" + "\"" + " : " + "\"" + _id + "\"" + ",");
            record.Append("\"" + "name" + "\"" + " : " + "\"" + _name + "\"" + ",");
            record.Append("\"" + "description" + "\"" + " : " + "\"" + _description + "\"" + ",");
            record.Append("\"" + "icon" + "\"" + " : " + "\"" + _icon + "\"");

            record.Append("}");

            return record.ToString();
        }

    }
}
