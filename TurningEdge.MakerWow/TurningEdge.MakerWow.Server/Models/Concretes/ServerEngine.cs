using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Debugging;
using TurningEdge.MakerWow.Api.Managers;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.DataTypes;
using TurningEdge.MakerWow.Models;
using TurningEdge.MakerWow.Server.Controllers;
using TurningEdge.MakerWow.Server.Interfaces;
using TurningEdge.Networking.Models.Abstracts;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.Serializing;
using TurningEdge.Web.Windows.WebContext.Concretes;

namespace TurningEdge.MakerWow.Server.Models.Concretes
{
    public class ServerEngine<T, W> : CommandAgent<T, W>, IEnginable
        where T : Session
        where W : NetworkInfo<T>
    {
        private Dictionary<int, Session> _clients;

        public ServerEngine(string ipAddress, int port)
            :base(ipAddress, port)
        {
            MakerWOWApi.Initialize<WindowsWebContext>();
            _clients = new Dictionary<int, Session>();
        }

        public void Start()
        {
            Debugger.Print("Starting...");
            _networkController.Networker.Connect();
            _networkController.Networker.OnDisconnected += Networker_OnDisconnected;
        }

        private void Networker_OnDisconnected(Session session)
        {
            int result = -1;
            foreach (var key in _clients.Keys)
            {
                if (session.Address == _clients[key].Address)
                    result = key;
            }
            if(result != -1)
                _clients.Remove(result);
        }

        public void Update()
        {
            Debugger.Print("Updating...");
            while(_commands.Count > 0)
            {
                ProcessCommand(_commands.Dequeue());
            }
        }

        public void Stop()
        {
            Debugger.Print("Stopping...");
            _networkController.Networker.Stop();
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
                    MakerWOWApi.Login(sessionCommand, (string)sessionCommand.CurrentCommand.Arguments[0], 
                        (string)sessionCommand.CurrentCommand.Arguments[1], onLoginSuccess, onLoginFailed);
                    break;
                case DataTypes.CommandType.Logout:
                    Debugger.Print("Processing command: Logout");
                    MakerWOWApi.Logout(sessionCommand, onLogoutSuccess, onLogoutFailed);
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
                    Debugger.Print("Processing command: Lobby");
                    foreach (var client in _clients.Values)
                    {
                        _networkController.Networker.Send(client, sessionCommand.CurrentCommand.ToBytes());
                    }
                    break;
                case DataTypes.CommandType.Editor:
                    Debugger.Print("Processing command: Editor");
                    break;
                default:
                    break;
            }
        }

        private void onLogoutFailed(SessionCommand sessionCommand, ApiContext context)
        {
            Command command = new Command(CommandType.Login, WorldCommand.None, false, context.Error.Message);
            _networkController.Networker.Send(sessionCommand.CurrentSession, command.ToBytes());
        }

        private void onLogoutSuccess(SessionCommand sessionCommand, ApiContext context)
        {
            Command command = new Command(CommandType.Login, WorldCommand.None, true);
            _networkController.Networker.Send(sessionCommand.CurrentSession, command.ToBytes());
        }

        private void onLoginFailed(SessionCommand sessionCommand, ApiContext context)
        {
            Command command = new Command(CommandType.Login, WorldCommand.None, false, context.Error.Message);
            _networkController.Networker.Send(sessionCommand.CurrentSession, command.ToBytes());
        }

        private void onLoginSuccess(SessionCommand sessionCommand, User user, ApiContext context)
        {
            _clients.Add(user.Id, sessionCommand.CurrentSession);
            Command command = new Command(CommandType.Login, WorldCommand.None, true, user.Clone());
            _networkController.Networker.Send(sessionCommand.CurrentSession, command.ToBytes());
        }
    }
}
