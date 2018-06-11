using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.Interfaces;
using TurningEdge.MakerWow.Models.Relationships;

namespace TurningEdge.MakerWow.Api.Models.Relationships
{
    [Serializable]
    public class RelationshipSkillUserJsonObject : RelationshipSkillUser, IJsonObject
    {

        public RelationshipSkillUserJsonObject(object record)
        {
            ParseJson(record as Dictionary<string, object>);
        }

        public RelationshipSkillUserJsonObject(int userId, int skillId, int experience) 
            : base(userId, skillId, experience)
        {
        }

        public void ParseJson(Dictionary<string, object> record)
        {
            _userId = int.Parse((string)record["user_id"]);
            _skillId = int.Parse((string)record["skill_id"]);
            _experience = int.Parse((string)record["experience"]);
        }

        public string SerializeJson()
        {
            var record = new StringBuilder();
            record.Append("{");

            record.Append("\"" + "user_id" + "\"" + " : " + "\"" + _userId + "\"" + ",");
            record.Append("\"" + "skill_id" + "\"" + " : " + "\"" + _skillId + "\"" + ",");
            record.Append("\"" + "experience" + "\"" + " : " + "\"" + _experience + "\"");

            record.Append("}");

            return record.ToString();
        }
        
    }
}
