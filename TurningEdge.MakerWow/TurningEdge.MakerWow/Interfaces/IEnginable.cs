using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Models;

namespace TurningEdge.MakerWow.Server.Interfaces
{
    public interface IEnginable
    {
        void Start();
        void Update();
        void Stop();
    }
}
