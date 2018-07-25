using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.Networking.Unity.Helpers;
using UnityEngine.Networking;

namespace TurningEdge.Networking.Unity.Models.Concretes
{
    public class UnityServerSession : UnitySession
    {

        public override bool IsConnected
        {
            get
            {
                return NetworkServer.active;
            }
        }

        /// <summary>
        /// Registers a handler.
        /// </summary>
        /// <param name="msg">The message type to register.</param>
        /// <param name="handler">The associated fired method to be executed on event.</param>
        public override void RegisterHandler(short msg, NetworkMessageDelegate handler)
        {
            NetworkServer.RegisterHandler(msg, handler);
        }

        public override void UnregisterHandler(short msg)
        {
            NetworkServer.UnregisterHandler(msg);
        }


        public UnityServerSession(string ipAddress, int port)
             : base(ipAddress, port)
        {
            DoTry(() =>
            {
                _ipAddress = ipAddress;
                _port = port;
                RegisterEvents();
            });
        }

        public override void Bind(string address, int port)
        {
            
        }

        public override void Connect(string address, int port)
        {
            Listen();
        }

        public override void Listen()
        {
            NetworkServer.Listen(_ipAddress, _port);
        }

        public override void Stop()
        {
            NetworkServer.DisconnectAll();
            NetworkServer.Shutdown();
        }

        protected override void ProcessSend(Packet packet)
        {
            NetworkMessage message = packet.ToNetworkMessage();
            NetworkServer.SendToAll(NetworkMessage.SEND_MESSAGE, message);
        }
    }
}
