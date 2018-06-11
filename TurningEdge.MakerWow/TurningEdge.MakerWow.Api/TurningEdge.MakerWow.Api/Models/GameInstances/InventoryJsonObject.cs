using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.Interfaces;
using TurningEdge.MakerWow.Models.GameInstances;
using TurningEdge.Serializers;
using TurningEdge.Serializing;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    [Serializable]
    public class InventoryJsonObject : Inventory, IJsonObject
    {
        public InventoryJsonObject(int id, int userId, int slotCount) 
            : base(id, userId, slotCount)
        {
        }

        public InventoryJsonObject(int id, int userId, string data) 
            : base(id, userId, data)
        {
        }

        public InventoryJsonObject(object record)
        {
            ParseJson(record as Dictionary<string, object>);
        }

        public void ParseJson(Dictionary<string, object> record)
        {
            _id = int.Parse((string)record["id"]);
            _userId = int.Parse((string)record["user_id"]);
            _data = (string)record["data"];
            _slots = _data.Decode().ToObject<Slot[]>();
        }

        public string SerializeJson()
        {
            var record = new StringBuilder();
            record.Append("{");

            record.Append("\"" + "id" + "\"" + " : " + "\"" + _id + "\"" + ",");
            record.Append("\"" + "user_id" + "\"" + " : " + "\"" + _userId + "\"" + ",");
            record.Append("\"" + "data" + "\"" + " : " + "\"" + _slots.ToBytes().Encode() + "\"");

            record.Append("}");

            return record.ToString();
        }
        
    }
}
