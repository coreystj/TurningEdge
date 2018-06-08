using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Api.Repositories.Abstracts;

namespace TurningEdge.MakerWow.Api.Repositories
{
    public class ChunkDataRepository : ApiRepository<ChunkData>
    {
        protected override string SetPrimaryData(List<string> primaryKeys)
        {
            primaryKeys.Add("user_id");
            primaryKeys.Add("world_layer_id");
            primaryKeys.Add("x_coordinate");
            primaryKeys.Add("y_coordinate");

            return "chunk_data";
        }
    }
}
