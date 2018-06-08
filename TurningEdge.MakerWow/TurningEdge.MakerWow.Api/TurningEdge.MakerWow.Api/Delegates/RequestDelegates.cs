using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Models;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.GameInstances;

namespace TurningEdge.MakerWow.Api.Delegates
{
    public delegate void OnLoginSuccessAction(User user, ApiContext context);
    public delegate void OnLoginFailedAction(ApiContext context);

    public delegate void OnLogoutSuccessAction(ApiContext context);
    public delegate void OnLogoutFailedAction(ApiContext context);

    public delegate void OnGetSuccessAction<T>(T[] worldLayers, ApiResult<T> context) where T : JsonObject;

    public delegate void OnSuccessAction(ApiAction context);
    public delegate void OnFailedAction(ApiContext context);

    public delegate void OnErrorAction(ApiException exception);
}
