using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    public class Inventory : JsonObject
    {
        private int _id;
        private int _userId;
        private string _data;

        public int Id
        {
            get { return _id; }
        }

        public int UserId
        {
            get { return _userId; }
        }

        public string Data
        {
            get { return _data; }
        }
        

        public Inventory(
            int id,
            int userId,
            string data)
            : base()
        {
            _id = id;
            _userId = userId;
            _data = data;
        }

        public Inventory(object record)
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

            var model = obj as Inventory;

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
            _data = (string)record["data"];
        }

        public override string SerializeJson()
        {
            var record = new StringBuilder();
            record.Append("{");

            record.Append("\"" + "id" + "\"" + " : " + "\"" + _id + "\"" + ",");
            record.Append("\"" + "user_id" + "\"" + " : " + "\"" + _userId + "\"" + ",");
            record.Append("\"" + "data" + "\"" + " : " + "\"" + _data + "\"");

            record.Append("}");

            return record.ToString();
        }
    }
}
