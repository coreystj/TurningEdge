using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Helpers;
using TurningEdge.Networking.LocalNetwork.Managers;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.Networking.WindowsSocket.Models.Concretes
{
    public class LocalSession : Session
    {
        private int _connectedId;
        public override bool IsConnected
        {
            get
            {
                return (_connectedId != -1);
            }
        }

        public LocalSession()
            : base()
        {
            _connectedId = -1;
        }

        public LocalSession(string ipAddress, int port)
            : base(ipAddress, port)
        {
            _connectedId = -1;
            _ipAddress = ipAddress;
            _port = port;
        }

        protected override void ProcessSend(Packet packet)
        {
            DoTry(() =>
            {
                FireOnMessageSendAttempt(this);
                OutBuffer = packet.ToBytes();

                if (LocalNetworkManager.Send(this, _connectedId, OutBuffer))
                {
                    if (OutGoingPackets.Count == 0)
                        FireOnMessageSentSuccess(this);
                }
                else
                {
                    FireOnError(new NetworkInfoException("Could not send data to end poin id: " 
                        + _connectedId));
                }
            });
        }

        public void ReadCallback(Session session, byte[] inBytes)
        {
            DoTry(() =>
            {
                Packet packet = inBytes.ToPacket();

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
            });
        }

        public override void Stop()
        {
            DoTry(() =>
            {
                _connectedId = -1;
                FireOnStopped(this);
            });
        }

        public override void Bind(string ipAddress, int port)
        {
            DoTry(() =>
            {
                _connectedId = LocalNetworkManager.BindServer(this);
            });
        }

        public override void Listen()
        {
            DoTry(() =>
            {
                FireOnListening(_ipAddress, _port);
            });
        }


        public void AcceptCallback(Session session)
        {
            DoTry(() =>
            {
                FireOnConnected(session);
            });

            Listen();
        }

        public override void Connect(string ipAddress, int port)
        {
            DoTry(() =>
            {
                FireOnConnectionAttempt(ipAddress, port);
                _connectedId = LocalNetworkManager.Connect(this);
                FireOnConnected(this);
            });
        }

        protected override void DoTry(Action action)
        {
            try
            {
                action();
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
