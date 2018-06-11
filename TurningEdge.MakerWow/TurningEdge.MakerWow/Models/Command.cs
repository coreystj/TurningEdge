using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.DataTypes;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.MakerWow.Models
{
    [Serializable]
    public class Command
    {
        private CommandType _commandType;
        private WorldCommand _worldCommand;
        private object[] _arguments;

        public CommandType CommandType
        {
            get { return _commandType; }
        }
        public object[] Arguments
        {
            get { return _arguments; }
        }

        public Command(
            CommandType commandType,
            params object[] arguments)
        {
            _commandType = commandType;
            _arguments = arguments;
        }
    }
}
