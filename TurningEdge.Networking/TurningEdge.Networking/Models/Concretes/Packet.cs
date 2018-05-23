using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Networking.DataTypes;

namespace TurningEdge.Networking.Models.Concretes
{
    public class Packet
    {
        private byte[] _bytes;
        private PacketType _type;

        public byte[] Bytes
        {
            get { return _bytes; }
        }
        public PacketType Type
        {
            get { return _type; }
        }

        public Packet(PacketType type, byte[] bytes)
        {
            _type = type;
            _bytes = bytes;
        }

    }
}
