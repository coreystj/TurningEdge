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

        protected override void ParseJson(Dictionary<string, object> record)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, object> SerializeJson()
        {
            var record = new Dictionary<string, object>();

            throw new NotImplementedException();

            return record;
        }
    }
}
