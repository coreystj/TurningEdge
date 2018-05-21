using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurningEdge.PathFinder.Common.DataTypes
{
    public enum TileType
    {
        BLOCKED_TILE = 0x0,
        EMPTY_TILE = 0x1
    }

    public enum NodeType
    {
        Start = 1,
        End = 2,
        Open = 4,
        Close = 8,
        Current = 16,
        Path = 32
    }
}
