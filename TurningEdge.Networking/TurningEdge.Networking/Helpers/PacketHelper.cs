using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Helpers;
using TurningEdge.Networking.DataTypes;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.Helpers
{
    public static class PacketHelper
    {
        public static Packet ToPacket(this byte[] bytes)
        {
            byte[] extracted = new byte[bytes.Length - 1];
            Array.Copy(bytes, 1, extracted, 0, extracted.Length);
            Packet packet = new Packet((PacketType)bytes[0], extracted);

            return packet;
        }

        public static byte[] ToBytes(this Packet packet)
        {
            byte[] extracted = new byte[packet.Bytes.Length + 1];
            Array.Copy(packet.Bytes, 0, extracted, 1, packet.Bytes.Length);
            extracted[0] = (byte)packet.Type;
            return extracted;
        }

        public static List<Packet> ToPackets(this byte[] bytes)
        {
            List<byte[]> bytePackets = bytes.Split(Session.BUFFER_SIZE - 1);
            List<Packet> packets = new List<Packet>();//[bytePackets.Count];
            int i = 0;
            PacketType type = PacketType.None;
            foreach (byte[] bytePacket in bytePackets)
            {
                switch (bytePackets.Count)
                {
                    case 0:
                        type = PacketType.None;
                        break;
                    case 1:
                        type = PacketType.Last;
                        break;
                    case 2:
                        if (i == 0)
                            type = PacketType.First;
                        else
                            type = PacketType.Last;
                        break;
                    case 3:
                        if (i == 0)
                            type = PacketType.First;
                        else if (i == 1)
                            type = PacketType.Segment;
                        else
                            type = PacketType.Last;
                        break;
                    default:
                        if (i == 0)
                            type = PacketType.First;
                        else if (i == bytePackets.Count - 1)
                            type = PacketType.Last;
                        else
                            type = PacketType.Segment;
                        break;
                }
                packets.Add(new Packet(type, bytePacket));
                i++;
            }

            return packets;
        }

        public static byte[] ToBytes(this List<Packet> packets)
        {
            int i = 0;
            byte[] allBytes = new byte[(packets.Count - 1) * Session.BUFFER_SIZE 
                + packets[packets.Count - 1].Bytes.Length];
            foreach (var packet in packets)
            {
                Array.Copy(packet.Bytes, 0, allBytes, i * Session.BUFFER_SIZE, packet.Bytes.Length);

                i++;
            }

            return allBytes;
        }
    }
}
