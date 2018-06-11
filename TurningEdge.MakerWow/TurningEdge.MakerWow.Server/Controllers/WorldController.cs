using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Server.Models.Abstracts;

namespace TurningEdge.MakerWow.Server.Controllers
{
    public class WorldController
    {
        public HashSet<World> _worlds;

        public WorldController()
        {
            _worlds = new HashSet<World>();
        }
    }
}
