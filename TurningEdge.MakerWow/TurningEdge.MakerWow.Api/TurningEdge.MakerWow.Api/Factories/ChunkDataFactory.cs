using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Models;
using TurningEdge.MakerWow.Models.GameInstances;

namespace TurningEdge.MakerWow.Api.Factories
{
    public class ChunkDataFactory
    {
        private User _currentUser;

        public ChunkDataFactory(User user)
        {
            _currentUser = user;
        }

        public ChunkData Create(WorldLayerJsonObject worldLayer, int x, int y)
        {
            ChunkData chunk = new ChunkData(0, _currentUser.Id, x, y, worldLayer.Id);
            return chunk;
        }
    }
}
