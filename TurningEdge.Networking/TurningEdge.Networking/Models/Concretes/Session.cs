using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TurningEdge.Networking.Helpers;

namespace TurningEdge.Networking.Models.Concretes
{
    public class Session
    {
        // Size of receive buffer.  
        public const int BUFFER_SIZE = 10;

        protected byte[] _inBuffer;
        protected byte[] _outBuffer;
        protected Socket _currentSocket;
        protected IPEndPoint _localEndPoint;

        protected string _ipAddress;
        protected int _port;

        protected List<Packet> _inComingPackets;
        protected List<Packet> _outGoingPackets;

        public List<Packet> OutGoingPackets
        {
            get
            {
                return _outGoingPackets;
            }
        }

        public string Address
        {
            get
            {
                return _ipAddress + ":" + _port;
            }
        }

        public byte[] InBuffer
        {
            get
            {
                return _inBuffer;
            }
            set
            {
                _inBuffer = value;
            }
        }
        public byte[] OutBuffer
        {
            get
            {
                return _outBuffer;
            }
            set
            {
                _outBuffer = value;
            }
        }

        public IPEndPoint LocalEndPoint
        {
            get { return _localEndPoint; }
            set { _localEndPoint = value; }
        }

        public Socket CurrentSocket
        {
            get
            {
                return _currentSocket;
            }
        }



        public Session()
        {
            _inBuffer = new byte[BUFFER_SIZE];
            _outBuffer = new byte[BUFFER_SIZE];
            _inComingPackets = new List<Packet>();
            _outGoingPackets = new List<Packet>();
        }

        public Session(Socket currentSocket)
            :this()
        {
            _currentSocket = currentSocket; 
            _localEndPoint = _currentSocket.LocalEndPoint as IPEndPoint;

            _ipAddress = _localEndPoint.Address.ToString();
            _port = _localEndPoint.Port;
        }

        public Session(string ipAddress, int port)
            : this()
        {
            _ipAddress = ipAddress;
            _port = port;
            IPAddress parsedIpAddress = IPAddress.Parse(ipAddress);
            _localEndPoint = new IPEndPoint(parsedIpAddress, port);

            _currentSocket = new Socket(parsedIpAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
        }

        public byte[] Append(Packet packet)
        {
            _inComingPackets.Add(packet);

            if(packet.Type == DataTypes.PacketType.Last)
            {
                byte[] allBytes = PacketHelper.ToBytes(_inComingPackets);
                _inComingPackets.Clear();
                return allBytes;
            }

            return null;
        }

        public Packet PopPacket()
        {
            if (_outGoingPackets.Count > 0)
            {
                Packet packet = _outGoingPackets[0];
                _outGoingPackets.RemoveAt(0);
                return packet;
            }
            return null;
        }

        public void SetOutGoingPackets(List<Packet> packets)
        {
            _outGoingPackets = packets;
        }

        public override string ToString()
        {
            return Address;
        }
    }
}
