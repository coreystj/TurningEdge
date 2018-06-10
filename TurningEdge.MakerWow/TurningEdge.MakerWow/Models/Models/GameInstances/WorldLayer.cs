using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Models.GameInstances
{
    public class WorldLayer
    {
        protected int _id;
        protected int _userId;
        protected string _name;
        protected string _description;
        protected int _environmentId;

        public int Id
        {
            get { return _id; }
        }

        public int UserId
        {
            get { return _userId; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public int EnvironmentId
        {
            get { return _environmentId; }
        }

        public WorldLayer()
        {

        }

        public WorldLayer(        
            int id,
            int userId,
            string name,
            string description,
            int environmentId)
            : base()
        {
            _id = id;
            _userId = userId;
            _name = name;
            _description = description;
            _environmentId = environmentId;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var model = obj as WorldLayer;

            return (model.GetHashCode() == GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode()
                    ^ UserId.GetHashCode();
        }
    }
}
