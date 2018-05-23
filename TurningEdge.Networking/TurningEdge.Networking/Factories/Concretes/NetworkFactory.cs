using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.Networking.DataTypes;
using TurningEdge.Networking.Models.Abstracts;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.Factories.Abstracts
{
    public class NetworkFactory<T> : Factory<T>
        where T : NetworkInfo
    {
        public NetworkFactory(params object[] initializers)
            : base(initializers)
        {

        }

        public static Packet CreatePacket(PacketType type, byte[] bytes)
        {
            return new Packet(type, bytes);
        }
    }
}
