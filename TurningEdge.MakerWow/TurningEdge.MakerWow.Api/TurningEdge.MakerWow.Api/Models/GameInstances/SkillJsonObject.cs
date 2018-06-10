using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.Interfaces;
using TurningEdge.MakerWow.Models.GameInstances;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    public class SkillJsonObject : Skill, IJsonObject
    {
        public SkillJsonObject(object record)
        {
            ParseJson(record as Dictionary<string, object>);
        }

        public SkillJsonObject(int id, string name, string description) 
            : base(id, name, description)
        {
        }

        public void ParseJson(Dictionary<string, object> record)
        {
            _id = int.Parse((string)record["id"]);
            _name = (string)record["name"];
            _description = (string)record["description"];
        }

        public string SerializeJson()
        {
            var record = new StringBuilder();
            record.Append("{");

            record.Append("\"" + "id" + "\"" + " : " + "\"" + _id + "\"" + ",");
            record.Append("\"" + "name" + "\"" + " : " + "\"" + _name + "\"" + ",");
            record.Append("\"" + "description" + "\"" + " : " + "\"" + _description + "\"");

            record.Append("}");

            return record.ToString();
        }
    }
}
