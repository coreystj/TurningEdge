using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Delegates;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Api.Repositories.Abstracts;

namespace TurningEdge.MakerWow.Api.Repositories
{
    public class ChunkDataRepository : ApiRepository<ChunkData>
    {
        public void Read(int layerId, int x, int y, 
            OnGetSuccessAction<ChunkData> onReadSuccessAction,
            OnFailedAction onReadFailedAction)
        {
            var filter = new Dictionary<string, string>();

            filter.Add("world_layer_id", layerId.ToString());
            filter.Add("x_coordinate", x.ToString());
            filter.Add("y_coordinate", y.ToString());

            Read(onReadSuccessAction, onReadFailedAction, filter);
        }

        public void Read(int layerId, 
            OnGetSuccessAction<ChunkData> onReadSuccessAction,
            OnFailedAction onReadFailedAction)
        {
            var filter = new Dictionary<string, string>();

            filter.Add("world_layer_id", layerId.ToString());

            Read(onReadSuccessAction, onReadFailedAction, filter);
        }

        protected override string SetPrimaryData(List<string> primaryKeys)
        {
            primaryKeys.Add("id");
            primaryKeys.Add("user_id");
            primaryKeys.Add("world_layer_id");
            primaryKeys.Add("x_coordinate");
            primaryKeys.Add("y_coordinate");

            return "chunk_data";
        }
    }
}
