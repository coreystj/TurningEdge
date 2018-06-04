using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    public class WorldData : JsonObject
    {
        private User _owner;
        private int _x;
        private int _y;
        private WorldLayer _layer;
        private byte[] _landscapeData;
        private byte[] _objectsData;

        public WorldData(object result)
            :base(result)
        {

        }

        protected override void ParseJson(object record)
        {
            _owner = 
        }
    }
}
