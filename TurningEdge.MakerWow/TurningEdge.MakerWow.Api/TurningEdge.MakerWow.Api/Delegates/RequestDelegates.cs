using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Models.GameInstances;

namespace TurningEdge.MakerWow.Api.Delegates
{
    public delegate void OnGetWorldDataSuccessAction(WorldData[] worldData);
    public delegate void OnGetWorldDataFailedAction(ApiException exception);

    public delegate void OnSetWorldDataSuccessAction();
    public delegate void OnSetWorldDataFailedAction(ApiException exception);
}
