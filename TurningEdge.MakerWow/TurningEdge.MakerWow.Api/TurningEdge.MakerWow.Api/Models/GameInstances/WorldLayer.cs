using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    public class WorldLayer : JsonObject
    {
        private int _id;
        private int _userId;
        private string _name;
        private string _description;
        private int _environmentId;

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

        public WorldLayer(object record) 
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

            var model = obj as WorldLayer;

            return (model.GetHashCode() == GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode()
                    ^ UserId.GetHashCode();
        }

        protected override void ParseJson(Dictionary<string, object> record)
        {
            _id = int.Parse((string)record["id"]);
            _userId = int.Parse((string)record["user_id"]);
            _name = (string)record["name"];
            _description = (string)record["description"];
            _environmentId = int.Parse((string)record["environment"]);
        }

        public override string SerializeJson()
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
