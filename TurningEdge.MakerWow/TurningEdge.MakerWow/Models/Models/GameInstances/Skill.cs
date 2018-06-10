using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Models.GameInstances
{
    public class Skill
    {
        protected int _id;
        protected string _name;
        protected string _description;

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }
        public Skill()
        {

        }

        public Skill(
            int id,
            string name,
            string description)
            : base()
        {
            _id = id;
            _name = name;
            _description = description;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var model = obj as Skill;

            return (model.GetHashCode() == GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
       
    }
}
