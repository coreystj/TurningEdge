using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Api.DataTypes
{
    [Serializable]
    public struct Landscape
    {
        public byte[] Color;
        public byte[] Height;
        public byte[] Ground;
    }
}
