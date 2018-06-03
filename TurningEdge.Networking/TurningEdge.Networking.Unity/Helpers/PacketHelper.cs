using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Networking.Helpers;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.Networking.Unity.Models.Concretes;

namespace TurningEdge.Networking.Unity.Helpers
{
    public static class PacketHelper
    {
        public static NetworkMessage ToNetworkMessage(this Packet packet)
        {
            NetworkMessage message = new NetworkMessage();
            message.Data = packet.ToBytes();
            return message;
        }
    }
}
