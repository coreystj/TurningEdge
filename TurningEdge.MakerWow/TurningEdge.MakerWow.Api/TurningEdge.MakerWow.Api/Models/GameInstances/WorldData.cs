using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.Serializers;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    public class WorldData : JsonObject
    {
        private int _userId;
        private int _x;
        private int _y;
        private int _layerId;
        private byte[] _landscapeData;
        private byte[] _objectsData;

        public WorldData(object result)
            :base(result)
        {

        }

        protected override void ParseJson(object rawObject)
        {
            var record = rawObject as Dictionary<string, object>;

            _userId = int.Parse((string)record["user_id"]);
            _layerId = int.Parse((string)record["world_layer_id"]);
            _x = int.Parse((string)record["x_coordinate"]);
            _y = int.Parse((string)record["y_coordinate"]);
            _landscapeData = Base64Serializer.Decode((string)record["landscape"]);
            _objectsData = Base64Serializer.Decode((string)record["objects"]);
        }
    }
}
