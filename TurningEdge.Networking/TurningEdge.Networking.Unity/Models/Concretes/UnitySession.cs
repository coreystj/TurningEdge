
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Debugging;
using TurningEdge.Networking.DataTypes;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Helpers;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.Networking.Unity.DataTypes;
using TurningEdge.Networking.Unity.Helpers;
using TurningEdge.Unity.Helpers;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

namespace TurningEdge.Networking.Unity.Models.Concretes
{
    public abstract class UnitySession : Session
    {
        protected NetworkProtocol _protocol;

        public UnitySession(string ipAddress, int port) 
            : base(ipAddress, port)
        {
            
        }

        /// <summary>
        /// Registers a handler.
        /// </summary>
        /// <param name="msg">The message type to register.</param>
        /// <param name="handler">The associated fired method to be executed on event.</param>
        public abstract void RegisterHandler(short msg, NetworkMessageDelegate handler);

        /// <summary>
        /// Unregisters a handler given the handler message.
        /// </summary>
        /// <param name="msg">The handler message to unregister.</param>
        public abstract void UnregisterHandler(short msg);

        protected override void DoTry(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                DebugHelper.PrintError(
                    new NetworkInfoException("A network error has occured.", e));
            }
        }

        /// <summary>
        /// Registers all necessary events to the event.
        /// </summary>
        public void RegisterEvents()
        {
            RegisterHandler(MsgType.Connect, FireOnUnityConnected);
            RegisterHandler(MsgType.Disconnect, FireOnUnityDisconnected);
            RegisterHandler(MsgType.Error, FireOnUnityError);
            RegisterHandler(NetworkMessage.SEND_MESSAGE, FireOnUnityMessageReceived);
        }

        private void FireOnUnityMessageReceived(UnityEngine.Networking.NetworkMessage netMsg)
        {
            NetworkMessage message = netMsg.ReadMessage<NetworkMessage>();
            Packet packet = message.Data.ToPacket();

            if (packet.Type != PacketType.None)
            {
                byte[] bytes = this.Append(packet);

                if (bytes != null)
                {
                    FireOnMessageReceivedSuccess(this, bytes);
                    if (_messages.Count > 0)
                        ProcessSend(_messages.Dequeue());
                }
                else
                    this.Send(new Packet(
                        PacketType.None, new byte[] { 0 }));
            }
            else
            {
                this.Send(this.PopPacket());
            }
        }

        private void FireOnUnityError(UnityEngine.Networking.NetworkMessage netMsg)
        {
            ErrorMessage error = netMsg.ReadMessage<ErrorMessage>();

            DebugHelper.PrintError(
                new NetworkInfoException("Error:" + error.errorCode));
        }

        private void FireOnUnityDisconnected(UnityEngine.Networking.NetworkMessage netMsg)
        {
            FireOnDisconnected(this);
        }

        private void FireOnUnityConnected(UnityEngine.Networking.NetworkMessage netMsg)
        {
            FireOnConnected(this);
        }
    }
}
