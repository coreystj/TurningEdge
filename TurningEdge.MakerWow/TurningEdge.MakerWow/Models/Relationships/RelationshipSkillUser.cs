using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Models.Relationships
{
    [Serializable]
    public class RelationshipSkillUser : ICloneable
    {
        protected int _userId;
        protected int _skillId;
        protected int _experience;

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

        public RelationshipSkillUser()
        {

        }

        public RelationshipSkillUser(
            int userId,
            int skillId,
            int experience)
            : base()
        {
            _userId = userId;
            _skillId = skillId;
            _experience = experience;
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
            return UserId.GetHashCode()
                    ^ SkillId.GetHashCode();
        }

        public object Clone()
        {
            return new RelationshipSkillUser(_userId,
                _skillId,
                _experience);
        }
    }
}
