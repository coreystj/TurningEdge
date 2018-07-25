using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.Networking.Unity.DataTypes;
using TurningEdge.Networking.Unity.Helpers;
using UnityEngine.Networking;

namespace TurningEdge.Networking.Unity.Models.Concretes
{
    public class UnityClientSession : UnitySession
    {
        private NetworkClient _connection;

        public override bool IsConnected
        {
            get
            {
                return _connection.isConnected;
            }
        }

        /// <summary>
        /// Registers a handler.
        /// </summary>
        /// <param name="msg">The message type to register.</param>
        /// <param name="handler">The associated fired method to be executed on event.</param>
        public override void RegisterHandler(short msg, NetworkMessageDelegate handler)
        {
            _connection.RegisterHandler(msg, handler);
        }

        public override void UnregisterHandler(short msg)
        {
            _connection.UnregisterHandler(msg);
        }

        public UnityClientSession(string ipAddress, int port)
            : base(ipAddress, port)
        {
            DoTry(() =>
            {
                _ipAddress = ipAddress;
                _port = port;
                _connection = new NetworkClient();
                RegisterEvents();
            });
        }

        public override void Bind(string address, int port)
        {
            throw new NotImplementedException();
        }

        public override void Connect(string address, int port)
        {
            _connection.Connect(_ipAddress, _port);
        }

        public override void Listen()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            DoTry(() =>
            {
                _connection.Disconnect();
                _connection.Shutdown();
            });
        }

        protected override void ProcessSend(Packet packet)
        {
            DoTry(() =>
            {
                FireOnMessageSendAttempt(this);
                var networkMessage = packet.ToNetworkMessage();

                _connection.Send(NetworkMessage.SEND_MESSAGE, networkMessage);
            });
        }
    }
}
