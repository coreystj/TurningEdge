using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Helpers;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.WindowsSocket.Models.Concretes
{
    public class SocketSession : Session
    {
        private Socket _currentSocket;

        public Socket CurrentSocket
        {
            get
            {
                return _currentSocket;
            }
        }

        public override bool IsConnected
        {
            get
            {
                if (_currentSocket != null)
                    return _currentSocket.Connected;
                else
                    return false;
            }
        }

        public SocketSession()
            : base()
        {
        }

        public SocketSession(Socket currentSocket, string ipAddress, int port)
            : this()
        {
            _currentSocket = currentSocket;

            _ipAddress = ipAddress;
            _port = port;
        }

        public SocketSession(string ipAddress, int port)
            : this()
        {
            DoTry(() =>
            {
                _ipAddress = ipAddress;
                _port = port;

                IPAddress parsedIpAddress = AddressHelper.Parse(_ipAddress);
                _currentSocket = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);
            });
        }

        protected override void ProcessSend(Packet packet)
        {
            DoTry(() =>
            {
                FireOnMessageSendAttempt(this);
                OutBuffer = packet.ToBytes();

                // Begin sending the data to the remote device.  
                CurrentSocket.BeginSend(
                    OutBuffer, 0, OutBuffer.Length, 0,
                    new AsyncCallback(SendCallback), this);
            });
        }

        protected void SendCallback(IAsyncResult ar)
        {
            DoTry(() =>
            {
                // Retrieve the socket from the state object.  
                SocketSession session = (SocketSession)ar.AsyncState;
                int bytesSent = session.CurrentSocket.EndSend(ar);
                if (OutGoingPackets.Count == 0)
                    FireOnMessageSentSuccess(this);
            });
        }

        public void ReadCallback(IAsyncResult ar)
        {
            DoTry(() =>
            {
                // Retrieve the state object and the handler socket  
                // from the asynchronous state object.  
                SocketSession session = (SocketSession)ar.AsyncState;

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
                        byte[] bytes = this.Append(packet);

                        if (bytes != null)
                        {
                            FireOnMessageReceivedSuccess(session, bytes);
                            if (_messages.Count > 0)
                                ProcessSend(_messages.Dequeue());
                        }
                        else
                            session.Send(new Packet(
                                DataTypes.PacketType.None, new byte[] { 0 }));
                    }
                    else
                    {
                        session.Send(session.PopPacket());
                    }
                }
            });
        }

        public override void Stop()
        {
            DoTry(() =>
            {
                this.CurrentSocket.Shutdown(SocketShutdown.Both);
                this.CurrentSocket.Close();
                FireOnStopped(this);
            });
        }

        public override void Bind(string ipAddress, int port)
        {
            DoTry(() =>
            {
                IPAddress ip = AddressHelper.Parse(ipAddress);
                IPEndPoint localEndPoint = new IPEndPoint(ip, port);

                CurrentSocket.Bind(localEndPoint);
                CurrentSocket.Listen(100);
            });
        }

        public override void Listen()
        {
            DoTry(() =>
            {
                FireOnListening(_ipAddress, _port);
                CurrentSocket.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    this);
            });
        }


        public void AcceptCallback(IAsyncResult ar)
        {
            DoTry(() =>
            {
                // Get the socket that handles the client request.  
                SocketSession session = (SocketSession)ar.AsyncState;

                Socket clientSocket = session.CurrentSocket.EndAccept(ar);

                // Create the state object.  
                Session clientSession = new SocketSession(clientSocket, _ipAddress, _port);

                clientSocket.BeginReceive(clientSession.InBuffer, 0, Session.BUFFER_SIZE, 0,
                    new AsyncCallback(ReadCallback), clientSession);
                FireOnConnected(session);
            });

            Listen();
        }

        public override void Connect(string ipAddress, int port)
        {
            DoTry(() =>
            {
                IPAddress ip = AddressHelper.Parse(ipAddress);
                IPEndPoint localEndPoint = new IPEndPoint(ip, port);

                FireOnConnectionAttempt(ipAddress, port);
                // Connect to the remote endpoint.  
                this.CurrentSocket.BeginConnect(localEndPoint,
                    new AsyncCallback(ConnectCallback), this);
            });
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            DoTry(() =>
            {
                // Retrieve the socket from the state object.  
                SocketSession session = (SocketSession)ar.AsyncState;

                // Complete the connection.  
                session.CurrentSocket.EndConnect(ar);

                session.CurrentSocket.BeginReceive(session.InBuffer, 0, Session.BUFFER_SIZE, 0,
                    new AsyncCallback(ReadCallback), session);

                FireOnConnected(session);
            });
        }

        protected override void DoTry(Action action)
        {
            try
            {
                action();
            }
            catch (SocketException sERef)
            {
                if (sERef.ErrorCode == 10054)
                    FireOnDisconnected(this);
                else if (sERef.ErrorCode == 10061 || sERef.ErrorCode == 10060)
                    FireOnConnectionFailed(Address, new NetworkInfoException(
                            "Failed to connect to local end point: " + this
                            + " ErrorCode: " + sERef.ErrorCode, sERef));
                else if (sERef.ErrorCode == 10048)
                    FireOnConnectionFailed(Address, new NetworkInfoException(
                            "Only one server can be hosted on this socket at a time: " + Address
                            + " ErrorCode: " + sERef.ErrorCode, sERef));
                else
                    FireOnError(
                        new NetworkInfoException(
                            "An error has occurred: " + this
                            + " code: " + sERef.ErrorCode, sERef));
            }
            catch (Exception e)
            {
                FireOnError(
                    new NetworkInfoException(
                        "An error has occurred: " + this, e));
            }
        }

        public override string ToString()
        {
            return Address;
        }
    }
}
