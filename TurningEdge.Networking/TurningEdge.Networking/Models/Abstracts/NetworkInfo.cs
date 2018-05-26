using System.Collections.Generic;
using TurningEdge.Networking.Delegates;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Factories.Abstracts;
using TurningEdge.Networking.Helpers;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.Models.Abstracts
{
    public abstract class NetworkInfo<T>
        where T : Session
    {
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

        protected Session _currentSession;
        protected string _ipAddress;
        protected int _port;

        public bool IsConnected
        {
            get
            {
                if (_currentSession != null)
                    return _currentSession.IsConnected;
                else
                    return false;
            }
        }

        public string Address
        {
            get
            {
                return _ipAddress + ":" + _port;
            }
        }

        public Session CurrentSession
        {
            get { return _currentSession; }
            set { _currentSession = value; }
        }

        public NetworkInfo(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
            _currentSession = NetworkFactory.CreateSession<T>(_ipAddress, _port);
            BindSession();
        }

        private void BindSession()
        {
            _currentSession.OnConnected += FireOnConnected;
            _currentSession.OnConnectionAttempt += FireOnConnectionAttempt;
            _currentSession.OnConnectionFailed += FireOnConnectionFailed;
            _currentSession.OnListening += FireOnListening;
            _currentSession.OnMessageReceivedSuccess += FireOnMessageReceivedSuccess;
            _currentSession.OnMessageSendAttempt += FireOnMessageSendAttempt;
            _currentSession.OnMessageSentSuccess += FireOnMessageSentSuccess;
            _currentSession.OnStopped += FireOnStopped;
            _currentSession.OnError += FireOnError;
            _currentSession.OnDisconnected += FireOnDisconnected;
        }

        public abstract void Connect();
        public void Send(Session session, byte[] bytes)
        {
            List<Packet> packets = bytes.ToPackets();
            Send(session, packets);
        }

        public void Send(Session session, List<Packet> packets)
        {
            session.SetOutGoingPackets(packets);
            Send(session, session.PopPacket());
        }

        public void Send(Session session, Packet packet)
        {
            session.Send(packet);
        }

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

        public void Stop()
        {
            _currentSession.Stop();
        }

        public override string ToString()
        {
            return _currentSession.ToString();
        }
    }
}
