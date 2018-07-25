using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Models.GameInstances
{
    [Serializable]
    public class Ground : ICloneable
    {
        protected int _id;
        protected string _name;
        protected string _description;
        protected string _icon;

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

        public string Icon
        {
            get { return _icon; }
        }
        
        public Ground()
        {

        }
        public Ground(
            int id,
            string name,
            string description,
            string icon
            )
            : base()
        {
            _id = id;
            _name = name;
            _description = description;
            _icon = icon;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var model = obj as Ground;

            return (model.GetHashCode() == GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public object Clone()
        {
            return new Ground(_id,
                _name,
                _description,
                _icon);
        }

    }
}
