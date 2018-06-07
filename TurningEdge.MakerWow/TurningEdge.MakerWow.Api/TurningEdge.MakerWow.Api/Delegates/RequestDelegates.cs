using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Models;
using TurningEdge.MakerWow.Api.Models.GameInstances;

namespace TurningEdge.MakerWow.Api.Delegates
{
    public delegate void OnLoginSuccessAction(User user);
    public delegate void OnLoginFailedAction(ApiException exception);

    public delegate void OnLogoutSuccessAction();
    public delegate void OnLogoutFailedAction(ApiException exception);

    public delegate void OnGetChunkDataSuccessAction(ChunkData[] worldData);
    public delegate void OnGetChunkDataFailedAction(ApiException exception);

    public delegate void OnSetChunkDataSuccessAction();
    public delegate void OnSetChunkDataFailedAction(ApiException exception);

    public delegate void OnGetWorldLayersSuccessAction(WorldLayer[] worldLayers);
    public delegate void OnGetWorldLayersFailedAction(ApiException exception);

    public delegate void OnSetWorldLayersSuccessAction();
    public delegate void OnSetWorldLayersFailedAction(ApiException exception);
}
