using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using TurningEdge.Networking.Delegates;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Helpers;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.Models.Abstracts
{
    public abstract class NetworkInfo
    {
        public event OnConnectedAction OnConnected = delegate { };
        public event OnConnectionFailedAction OnConnectionFailed = delegate { };
        public event OnDisconnectedAction OnDisconnected = delegate { };
        public event OnMessageSentSuccessAction OnMessageSentSuccess  = delegate { };
        public event OnMessageReceivedSuccessAction OnMessageReceivedSuccess = delegate { };
        public event OnErrorAction OnError = delegate { };
        public event OnStoppedAction OnStopped  = delegate { };

        protected Session _currentSession;
        protected string _ipAddress;
        protected int _port;

        public bool IsConnected
        {
            get
            {
                if (_currentSession != null)
                    if(_currentSession.CurrentSocket != null)
                        return _currentSession.CurrentSocket.Connected;
                    else
                        return false;
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
            DoTry(() => {
                _currentSession = new Session(_ipAddress, _port);
            });
        }

        public abstract void Connect();
        public void Send(Session session, byte[] bytes)
        {
            DoTry(() => {
                List<Packet> packets = bytes.ToPackets();

                Send(session, packets);
            });
        }

        public void Send(Session session, List<Packet> packets)
        {
            DoTry(() => {
                session.SetOutGoingPackets(packets);
                Send(session, session.PopPacket());
            });
        }

        public void Send(Session session, Packet packet)
        {
            DoTry(() => {
                session.OutBuffer = packet.ToBytes();

                // Begin sending the data to the remote device.  
                session.CurrentSocket.BeginSend(
                    session.OutBuffer, 0, session.OutBuffer.Length, 0,
                    new AsyncCallback(SendCallback), session);

                if (packet.Type == DataTypes.PacketType.Last)
                    FireOnMessageSentSuccess(session);
            });
        }

        public void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            Session session = (Session)ar.AsyncState;

            DoTry(() => {
                // Read data from the client socket.   
                int bytesRead = session.CurrentSocket.EndReceive(ar);
                byte[] inBytes = new byte[bytesRead];
                if (bytesRead > 0)
                {
                    Array.Copy(session.InBuffer, 0, inBytes, 0, bytesRead);
                    Packet packet = inBytes.ToPacket();

                    // Not all data received. Get more.  
                    session.CurrentSocket.BeginReceive(
                        session.InBuffer, 0, Session.BUFFER_SIZE, 0,
                        new AsyncCallback(ReadCallback), session);

                    if (packet.Type != DataTypes.PacketType.None)
                    {
                        byte[] bytes = _currentSession.Append(packet);

                        if (bytes != null)
                        {
                            FireOnMessageReceivedSuccess(session, bytes);
                        }
                        else
                            Send(session, new Packet(
                            DataTypes.PacketType.None, new byte[] { 0 }));
                    }
                    else
                    {
                        Send(session, session.PopPacket());
                    }


                }
            });
        }

        protected void SendCallback(IAsyncResult ar)
        {
            // Retrieve the socket from the state object.  
            Session session = (Session)ar.AsyncState;
            DoTry(() => {
                // Complete sending the data to the remote device.  
                int bytesSent = session.CurrentSocket.EndSend(ar);
            });
        }

        public void FireOnConnected(Session session)
        {
            DoTry(() =>
            {
                OnConnected(session);
            });
        }

        public void FireOnError(NetworkInfoException exception)
        {
            DoTry(() =>
            {
                OnError(exception);
            });
        }

        public void FireOnConnectionFailed(
            string address, NetworkInfoException exception)
        {
            DoTry(() =>
            {
                OnConnectionFailed(address, exception);
            });
        }

        public void FireOnMessageSentSuccess(Session session)
        {
            DoTry(() =>
            {
                OnMessageSentSuccess(session);
            });
        }

        public void FireOnMessageReceivedSuccess(Session session, byte[] bytes)
        {
            DoTry(() =>
            {
                OnMessageReceivedSuccess(session, bytes);
            });
        }

        public void FireOnStopped(Session session)
        {
            DoTry(() =>
            {
                OnStopped(session);
            });
        }

        public void FireOnDisconnected(Session session)
        {
            DoTry(() =>
            {
                OnDisconnected(session);
            });
        }

        public void Stop()
        {
            DoTry(() => {
                _currentSession.CurrentSocket.Shutdown(SocketShutdown.Both);
                _currentSession.CurrentSocket.Close();
            });
            FireOnStopped(_currentSession);
        }

        public override string ToString()
        {
            return _currentSession.ToString();
        }

        public void DoTry(Action action)
        {
            try
            {
                action();
            }
            catch (SocketException sERef)
            {
                if (sERef.ErrorCode == 10054)
                    FireOnDisconnected(_currentSession);
                else if (sERef.ErrorCode == 10061)
                    FireOnConnectionFailed(Address, new NetworkInfoException(
                            "Failed to connect to local end point: " + _currentSession
                            + " ErrorCode: " + sERef.ErrorCode, sERef));
                else if (sERef.ErrorCode == 10048)
                    FireOnConnectionFailed(Address, new NetworkInfoException(
                            "Only one server can be hosted on this socket at a time: " + Address
                            + " ErrorCode: " + sERef.ErrorCode, sERef));
                else
                    FireOnError(
                        new NetworkInfoException(
                            "An error has occurred: " + _currentSession 
                            + " code: " + sERef.ErrorCode, sERef));
            }
            catch (Exception e)
            {
                FireOnError(
                    new NetworkInfoException(
                        "An error has occurred: " + _currentSession, e));
            }
        }
    }
}
