using System;
using System.Collections.Generic;
using TurningEdge.Networking.Delegates;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Helpers;

namespace TurningEdge.Networking.Models.Concretes
{
    public abstract class Session
    {
        protected Guid _id;
        protected Queue<Packet> _messages;
        public event OnListeningAction OnListening = delegate { };
        public event OnConnectionAttemptAction OnConnectionAttempt = delegate { };
        public event OnConnectedAction OnConnected = delegate { };
        public event OnConnectionFailedAction OnConnectionFailed = delegate { };
        public event OnMessageSendAttemptAction OnMessageSendAttempt = delegate { };
        public event OnMessageSentSuccessAction OnMessageSentSuccess = delegate { };
        public event OnMessageReceivedSuccessAction OnMessageReceivedSuccess = delegate { };
        public event OnStoppedAction OnStopped = delegate { };
        public event OnDisconnectedAction OnDisconnected = delegate { };
        public event OnErrorAction OnError = delegate { };

        // Size of receive buffer.  
        public const int BUFFER_SIZE = 2;

        protected byte[] _inBuffer;
        protected byte[] _outBuffer;

        protected string _ipAddress;
        protected int _port;

        protected List<Packet> _inComingPackets;
        protected List<Packet> _outGoingPackets;

        public abstract bool IsConnected
        {
            get;
        }

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

        public Guid Id
        {
            get
            {
                return _id;
            }
        }

        public Session()
        {
            _id = Guid.NewGuid();
            _messages = new Queue<Packet>();
            _inBuffer = new byte[BUFFER_SIZE];
            _outBuffer = new byte[BUFFER_SIZE];
            _inComingPackets = new List<Packet>();
            _outGoingPackets = new List<Packet>();
        }

        public Session(string ipAddress, int port)
            : this()
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public byte[] Append(Packet packet)
        {
            _inComingPackets.Add(packet);

            if (packet.Type == DataTypes.PacketType.Last)
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

        protected abstract void ProcessSend(Packet packet);
        public void Send(Packet packet)
        {
            _messages.Enqueue(packet);

            if(_messages.Count == 1)
                ProcessSend(_messages.Dequeue());
        }

        

        public abstract void Stop();

        public abstract void Bind(string address, int port);

        public abstract void Listen();


        public abstract void Connect(string address, int port);

        protected abstract void DoTry(Action action);

        public void FireOnListening(string address, int port)
        {
            OnListening(address, port);
        }

        public void FireOnConnectionAttempt(string address, int port)
        {
            OnConnectionAttempt(address, port);
        }

        public void FireOnConnected(Session session)
        {
            OnConnected(session);
        }

        public void FireOnError(NetworkInfoException exception)
        {
            OnError(exception);
        }

        public void FireOnConnectionFailed(
            string address, NetworkInfoException exception)
        {
            OnConnectionFailed(address, exception);
        }

        public void FireOnMessageSentSuccess(Session session)
        {
            OnMessageSentSuccess(session);
        }

        public void FireOnMessageSendAttempt(Session session)
        {
            OnMessageSendAttempt(session);
        }

        public void FireOnMessageReceivedSuccess(Session session, byte[] bytes)
        {
            OnMessageReceivedSuccess(session, bytes);
        }

        public void FireOnStopped(Session session)
        {
            OnStopped(session);
        }

        public void FireOnDisconnected(Session session)
        {
            OnDisconnected(session);
        }

        public override string ToString()
        {
            return Address;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Session other = obj as Session;

            return other.GetHashCode() == GetHashCode();
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}
