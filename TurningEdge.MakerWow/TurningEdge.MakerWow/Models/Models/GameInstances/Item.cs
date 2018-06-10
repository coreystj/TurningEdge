using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Models.GameInstances
{
    public class Item
    {
        protected int _id;
        protected string _name;
        protected string _description;
        protected string _icon;
        protected string _model;

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

        public string Model
        {
            get { return _model; }
        }

        public Item()
        {

        }
        public Item(
            int id,
            string name,
            string description,
            string icon,
            string model
            )
            : base()
        {
            _id = id;
            _name = name;
            _description = description;
            _icon = icon;
            _model = model;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var model = obj as Item;

            return (model.GetHashCode() == GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        
    }
}
