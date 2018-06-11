using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TurningEdge.Debugging;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Managers;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Models;
using TurningEdge.MakerWow.Server.Controllers;
using TurningEdge.MakerWow.Server.Models.Concretes;
using TurningEdge.Networking.Factories.Abstracts;
using TurningEdge.Networking.Models.Abstracts;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.Networking.WindowsSocket.Models.Concretes;
using TurningEdge.Web.Windows.WebContext.Concretes;

namespace TurningEdge.MakerWow.Api.Pipeline
{
    public static class Class1
    {
        private static ServerEngine<SocketSession, Server<SocketSession>> _engine;

        private static void Main(string[] args)
        {
            _engine = new ServerEngine<SocketSession, Server<SocketSession>>("127.0.0.1", 3456);
            Start();
        }
        
        private static void Start()
        {
            try
            {
                _engine.Start();
            }
            catch (Exception e)
            {
                Debugger.PrintError(e);
            }
            while (true)
            {
                Thread.Sleep(1000);
                try
                {
                    _engine.Update();
                }
                catch (Exception e)
                {
                    Debugger.PrintError(e);
                    break;
                }
            }

            try
            {
                _engine.Stop();
            }
            catch (Exception e)
            {
                Debugger.PrintError(e);
            }
        }

        private static void OnCrudFailed(ApiContext context)
        {
            Debugger.PrintError(context.Error);
        }

        private static void MakerWOWApi_OnError(ApiException exception)
        {
            Debugger.PrintError(exception);
        }
    }
}
