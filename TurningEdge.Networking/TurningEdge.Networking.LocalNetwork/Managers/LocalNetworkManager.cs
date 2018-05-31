using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Networking.Models.Concretes;
using TurningEdge.Networking.WindowsSocket.Models.Concretes;

namespace TurningEdge.Networking.LocalNetwork.Managers
{
    public static class LocalNetworkManager
    {
        private static Session _serverSession;
        private static int _currentId = 0;
        private static Dictionary<int, LocalSession> _connections;

        static LocalNetworkManager()
        {
            _connections = new Dictionary<int, LocalSession>();
        }

        public static int Connect(LocalSession session)
        {
            _connections.Add(_currentId, session);
            return _currentId++;
        }

        public static void Disconnect(int id)
        {
            _connections.Remove(id);
        }

        public static bool Send(Session currentSession, int otherId, byte[] outBuffer)
        {
            LocalSession session;
            if(_connections.TryGetValue(otherId, out session))
            {
                session.ReadCallback(currentSession, outBuffer);
                return true;
            }
            return false;
        }

        public static int BindServer(Session server)
        {
            _serverSession = server;
            return -2;
        }
    }
}
