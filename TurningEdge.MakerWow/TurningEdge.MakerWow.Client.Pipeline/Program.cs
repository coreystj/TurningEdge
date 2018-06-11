using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TurningEdge.MakerWow.Api.Managers;
using TurningEdge.MakerWow.Client.Models;
using TurningEdge.MakerWow.DataTypes;
using TurningEdge.MakerWow.Models;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.Networking.WindowsSocket.Models.Concretes;
using TurningEdge.Web.Windows.WebContext.Concretes;

namespace TurningEdge.MakerWow.Api.Pipeline
{
    public static class Program
    {
        private static ClientEngine<SocketSession, Client<SocketSession>> _engine;

        private static void Main(string[] args)
        {
            MakerWOWApi.Initialize<WindowsWebContext>();
            _engine = new ClientEngine<SocketSession, Client<SocketSession>>("127.0.0.1", 3456);
            _engine.Start();

            Console.Write("Username: " );
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Login(_engine.ServerSession, username, password);
        }

        private static void Login(Session session, string username, string password)
        {
            Command command = new Command(CommandType.Login,
                username, password);
            _engine.SendCommand(command);
            MessangerTest();
        }

        private static void MessangerTest()
        {
            while(true)
            {
                Console.Write("Send Message: ");
                Command command = new Command(CommandType.Lobby,
                Console.ReadLine());
                _engine.SendCommand(command);
            }
        }
    }
}
