using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.MakerWow.Models
{
    public class SessionCommand
    {
        private Command _command;
        private Session _session;

        public Command CurrentCommand
        {
            get
            {
                return _command;
            }
        }

        public Session CurrentSession
        {
            get
            {
                return _session;
            }
        }

        public SessionCommand(Session session, Command command)
        {
            _command = command;
            _session = session;
        }
    }
}
