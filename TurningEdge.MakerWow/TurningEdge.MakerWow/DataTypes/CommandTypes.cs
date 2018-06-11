using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.DataTypes
{
    public enum CommandType
    {
        None,
        Login,
        Logout,
        StartWorld,
        StopWorld,
        World,
        Lobby,
        Editor
    }
}
