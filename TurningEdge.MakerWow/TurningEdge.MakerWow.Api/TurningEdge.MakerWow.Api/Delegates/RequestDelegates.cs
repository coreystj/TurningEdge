using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Models.GameInstances;

namespace TurningEdge.MakerWow.Api.Delegates
{
    public delegate void OnGetChunkDataSuccessAction(ChunkData[] worldData);
    public delegate void OnGetChunkDataFailedAction(ApiException exception);

    public delegate void OnSetChunkDataSuccessAction();
    public delegate void OnSetChunkDataFailedAction(ApiException exception);
}
