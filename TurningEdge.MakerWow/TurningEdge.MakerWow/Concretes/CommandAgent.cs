using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Debugging;
using TurningEdge.MakerWow.Interfaces;
using TurningEdge.MakerWow.Models;
using TurningEdge.MakerWow.Server.Controllers;
using TurningEdge.MakerWow.Server.Interfaces;
using TurningEdge.Networking.Models.Abstracts;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.MakerWow.Server.Models.Concretes
{
    public abstract class CommandAgent<T, W> : ICommandable
        where T : Session
        where W : NetworkInfo<T>
    {
        protected Queue<SessionCommand> _commands;
        protected NetworkController<T, W> _networkController;

        public CommandAgent(string ipAddress, int port)
        {
            _commands = new Queue<SessionCommand>();

            _networkController = new NetworkController<T, W>(
                this, ipAddress, port);
        }

        public abstract void ProcessCommand(SessionCommand sessionCommand);

        public void QueueCommand(SessionCommand sessionCommand)
        {
            _commands.Enqueue(sessionCommand);
        }
    }
}
