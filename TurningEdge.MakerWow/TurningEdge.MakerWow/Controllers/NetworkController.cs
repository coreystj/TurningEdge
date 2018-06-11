using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Debugging;
using TurningEdge.MakerWow.Models;
using TurningEdge.MakerWow.Server.Interfaces;
using TurningEdge.MakerWow.Server.Models.Concretes;
using TurningEdge.Networking.Exceptions;
using TurningEdge.Networking.Factories.Abstracts;
using TurningEdge.Networking.Models.Abstracts;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.Serializing;

namespace TurningEdge.MakerWow.Server.Controllers
{
    public class NetworkController<T, W>
        where T : Session
        where W : NetworkInfo<T>
    {
        private CommandAgent<T, W> _commandAgent;
        private NetworkInfo<T> _networker;

        public NetworkInfo<T> Networker
        {
            get
            {
                return _networker;
            }
        }

        public NetworkController(CommandAgent<T, W> commandAgent, string ipAddress, int port)
        {
            _networker = NetworkFactory.CreateNetworker<T, W>(ipAddress, port);
            _commandAgent = commandAgent;

            _networker.OnConnected += _networker_OnConnected;
            _networker.OnDisconnected += _networker_OnDisconnected;
            _networker.OnError += _networker_OnError;
            _networker.OnMessageReceivedSuccess += _networker_OnMessageReceivedSuccess;
        }

        private void _networker_OnMessageReceivedSuccess(Session session, byte[] bytes)
        {
            Command command = bytes.ToObject<Command>();
            _commandAgent.QueueCommand(new SessionCommand(session, command));
        }

        private void _networker_OnError(NetworkInfoException exception)
        {
            Debugger.PrintError(exception);
        }

        private void _networker_OnDisconnected(Session session)
        {
            Debugger.Print("Disconnected: " + session);
        }

        private void _networker_OnConnected(Session session)
        {
            Debugger.Print("Connected: " + session);
        }
    }
}
