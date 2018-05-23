using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TurningEdge.Networking.Models.Concretes
{
    public class Session
    {
        protected byte[] _inBuffer;
        protected byte[] _outBuffer;
        protected Socket _currentSocket;
        protected IPEndPoint _localEndPoint;

        protected string _ipAddress;
        protected int _port;

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

        // Size of receive buffer.  
        public const int BUFFER_SIZE = 1024;

        public Session()
        {
            _inBuffer = new byte[BUFFER_SIZE];
            _outBuffer = new byte[BUFFER_SIZE];
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
            IPEndPoint _localEndPoint = new IPEndPoint(parsedIpAddress, port);

            _currentSocket = new Socket(parsedIpAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
        }

        public override string ToString()
        {
            return Address;
        }
    }
}
