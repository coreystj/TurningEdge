using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Debugging;
using TurningEdge.MakerWow.Models;
using TurningEdge.MakerWow.Server.Models.Concretes;
using TurningEdge.Networking.Models.Abstracts;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.Serializing;

namespace TurningEdge.MakerWow.Client.Models
{
    public class ClientEngine<T, W> : CommandAgent<T, W>
        where T : Session
        where W : NetworkInfo<T>
    {
        private Session _serverSession;

        public Session ServerSession
        {
            get
            {
                return _serverSession;
            }
        }

        public ClientEngine(string ipAddress, int port)
            :base(ipAddress, port)
        {
        }

        public void Start()
        {
            _networkController.Networker.Connect();
            _networkController.Networker.OnMessageReceivedSuccess += DeQueueCommand;
            _serverSession = _networkController.Networker.CurrentSession;
        }

        private void DeQueueCommand(Session session, byte[] bytes)
        {
            while (_commands.Count > 0)
            {
                ProcessCommand(_commands.Dequeue());
            }
        }

        public void Stop()
        {
            _networkController.Networker.Stop();
        }

        public void SendCommand(Command command)
        {
            _networkController.Networker.Send(_networkController.Networker.CurrentSession, 
                command.ToBytes());
        }

        public override void ProcessCommand(SessionCommand sessionCommand)
        {
            switch (sessionCommand.CurrentCommand.CommandType)
            {
                case DataTypes.CommandType.None:
                    Debugger.Print("Processing command: None");
                    break;
                case DataTypes.CommandType.Login:
                    Debugger.Print("Processing command: Login");
                    break;
                case DataTypes.CommandType.Logout:
                    Debugger.Print("Processing command: Logout");
                    break;
                case DataTypes.CommandType.StartWorld:
                    Debugger.Print("Processing command: StartWorld");
                    break;
                case DataTypes.CommandType.StopWorld:
                    Debugger.Print("Processing command: StopWorld");
                    break;
                case DataTypes.CommandType.World:
                    Debugger.Print("Processing command: World");
                    break;
                case DataTypes.CommandType.Lobby:
                    Debugger.Print("Processing command: Lobby -> " + sessionCommand.CurrentCommand.Arguments[0]);
                    break;
                case DataTypes.CommandType.Editor:
                    Debugger.Print("Processing command: Editor");
                    break;
                default:
                    break;
            }
        }
    }
}
