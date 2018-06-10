using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;

namespace TurningEdge.MakerWow.Api.Models.Relationships
{
    public class RelationshipSkillUser : JsonObject
    {
        private int _id;
        private int _userId;
        private int _skillId;
        private int _experience;

        public int Id
        {
            get { return _id; }
        }

        public int UserId
        {
            get { return _userId; }
        }

        public int SkillId
        {
            get { return _skillId; }
        }

        public int Experience
        {
            get { return _experience; }
        }


        public RelationshipSkillUser(
            int id,
            int userId,
            int skillId,
            int experience)
            : base()
        {
            _id = id;
            _userId = userId;
            _skillId = skillId;
            _experience = experience;
        }

        public RelationshipSkillUser(object record)
            : base(record)
        {
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var model = obj as RelationshipSkillUser;

            return (model.GetHashCode() == GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode()
                    ^ UserId.GetHashCode()
                    ^ SkillId.GetHashCode()
                    ^ Experience.GetHashCode();
        }

        protected override void ParseJson(Dictionary<string, object> record)
        {
            _id = int.Parse((string)record["id"]);
            _userId = int.Parse((string)record["user_id"]);
            _skillId = int.Parse((string)record["skill_id"]);
            _experience = int.Parse((string)record["experience"]);
        }

        public override string SerializeJson()
        {
            var record = new StringBuilder();
            record.Append("{");

            record.Append("\"" + "id" + "\"" + " : " + "\"" + _id + "\"" + ",");
            record.Append("\"" + "user_id" + "\"" + " : " + "\"" + _userId + "\"" + ",");
            record.Append("\"" + "skill_id" + "\"" + " : " + "\"" + _skillId + "\"" + ",");
            record.Append("\"" + "experience" + "\"" + " : " + "\"" + _experience + "\"");

            record.Append("}");

            return record.ToString();
        }
    }
}
