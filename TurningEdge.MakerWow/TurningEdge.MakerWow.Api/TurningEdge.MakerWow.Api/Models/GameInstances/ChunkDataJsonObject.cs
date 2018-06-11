using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.MakerWow.DataTypes;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.Interfaces;
using TurningEdge.MakerWow.Models.GameInstances;
using TurningEdge.Serializers;
using TurningEdge.Serializing;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    [Serializable]
    public class ChunkDataJsonObject : ChunkData, IJsonObject
    {
        public ChunkDataJsonObject(int id, int userId, int x, int y, int layerId) 
            : base(id, userId, x, y, layerId)
        {
        }

        public ChunkDataJsonObject(object result) 
        {
            ParseJson(result as Dictionary<string, object>);
        }

        public void ParseJson(Dictionary<string, object> record)
        {
            _id = int.Parse((string)record["id"]);
            _userId = int.Parse((string)record["user_id"]);
            _layerId = int.Parse((string)record["world_layer_id"]);
            _x = int.Parse((string)record["x_coordinate"]);
            _y = int.Parse((string)record["y_coordinate"]);
            _landscapeBuffer = ((string)record["landscape"]).Decode().ToObject<Landscape>();
            _constructionBuffer = ((string)record["objects"]).Decode().ToObject<Construction>();
        }

        public string SerializeJson()
        {
            var record = new StringBuilder();

            record.Append("{");

            record.Append("\"" + "id" + "\"" + " : " + "\"" + _id + "\"" + ",");
            record.Append("\"" + "user_id" + "\"" + " : " + "\"" + _userId + "\"" + ",");
            record.Append("\"" + "world_layer_id" + "\"" + " : " + "\"" + _layerId + "\"" + ",");
            record.Append("\"" + "x_coordinate" + "\"" + " : " + "\"" + _x + "\"" + ",");
            record.Append("\"" + "y_coordinate" + "\"" + " : " + "\"" + _y + "\"" + ",");
            record.Append("\"" + "landscape" + "\"" + " : " + "\"" + _landscapeBuffer.ToBytes().Encode() + "\"" + ",");
            record.Append("\"" + "objects" + "\"" + " : " + "\"" + _constructionBuffer.ToBytes().Encode() + "\"");

            record.Append("}");

            return record.ToString();
        }
    }
}
