using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.MakerWow.Api.DataTypes;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.Serializers;
using TurningEdge.Serializing;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    public class ChunkData : JsonObject
    {
        private int _userId;
        private int _x;
        private int _y;
        private int _layerId;

        private Landscape _landscapeBuffer;
        private Construction _constructionBuffer;

        public int UserId
        {
            get
            {
                return _userId;
            }
        }
        public int X
        {
            get
            {
                return _x;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
        }

        public int LayerId
        {
            get
            {
                return _layerId;
            }
        }

        public Landscape LandscapeBuffer
        {
            get
            {
                return _landscapeBuffer;
            }
        }
        public Construction ConstructionBuffer
        {
            get
            {
                return _constructionBuffer;
            }
        }

        public ChunkData(        
            int userId,
            int x,
            int y,
            int layerId)
            : base()
        {
            _userId = userId;
            _x = x;
            _y = y;
            _layerId = layerId;

            _landscapeBuffer.Color = new byte[256];
            _landscapeBuffer.Ground = new byte[256];
            _landscapeBuffer.Height = new byte[256];

            _constructionBuffer.ConstructionId = new byte[256];
            _constructionBuffer.RotationIndex = new byte[256];
        }

        public ChunkData(object result)
            :base(result)
        {

        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var model = obj as ChunkData;

            return (model.GetHashCode() == GetHashCode());
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return LayerId.GetHashCode() 
                    ^ UserId.GetHashCode() 
                    ^ X.GetHashCode() 
                    ^ Y.GetHashCode();
        }

        protected override void ParseJson(Dictionary<string, object> record)
        {
            _userId = int.Parse((string)record["user_id"]);
            _layerId = int.Parse((string)record["world_layer_id"]);
            _x = int.Parse((string)record["x_coordinate"]);
            _y = int.Parse((string)record["y_coordinate"]);
            _landscapeBuffer = ((string)record["landscape"]).Decode().ToObject<Landscape>();
            _constructionBuffer = ((string)record["objects"]).Decode().ToObject<Construction>();
        }

        public override string SerializeJson()
        {
            var record = new StringBuilder();

            record.Append("{");

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
