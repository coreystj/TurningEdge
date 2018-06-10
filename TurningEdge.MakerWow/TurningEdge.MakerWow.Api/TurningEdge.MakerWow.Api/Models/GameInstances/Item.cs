using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    public class Item : JsonObject
    {
        private int _id;
        private string _name;
        private string _description;
        private string _icon;
        private string _model;

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

        public Item(object record)
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

            var model = obj as Item;

            return (model.GetHashCode() == GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        protected override void ParseJson(Dictionary<string, object> record)
        {
            _id = int.Parse((string)record["id"]);
            _name = (string)record["name"];
            _description = (string)record["description"];
            _icon = (string)record["icon"];
            _model = (string)record["model"];
        }

        public override string SerializeJson()
        {
            var record = new StringBuilder();
            record.Append("{");

            record.Append("\"" + "id" + "\"" + " : " + "\"" + _id + "\"" + ",");
            record.Append("\"" + "name" + "\"" + " : " + "\"" + _name + "\"" + ",");
            record.Append("\"" + "description" + "\"" + " : " + "\"" + _description + "\"");
            record.Append("\"" + "icon" + "\"" + " : " + "\"" + _icon+ "\"");
            record.Append("\"" + "model" + "\"" + " : " + "\"" + _model + "\"");

            record.Append("}");

            return record.ToString();
        }
    }
}
