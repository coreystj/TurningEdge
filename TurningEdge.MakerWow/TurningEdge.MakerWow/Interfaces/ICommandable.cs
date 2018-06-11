using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Models;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.MakerWow.Interfaces
{
    public interface ICommandable
    {
        void ProcessCommand(SessionCommand sessionCommand);
    }
}
