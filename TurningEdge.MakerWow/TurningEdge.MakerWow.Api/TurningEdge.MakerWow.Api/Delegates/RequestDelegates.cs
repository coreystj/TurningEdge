using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Models;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Api.Models.Interfaces;
using TurningEdge.MakerWow.Models;
using TurningEdge.Networking.Models.Concretes;

namespace TurningEdge.MakerWow.Api.Delegates
{
    public delegate void OnLoginSuccessAction(SessionCommand sessionCommand, User user, ApiContext context);
    public delegate void OnLoginFailedAction(SessionCommand sessionCommand, ApiContext context);

    public delegate void OnLogoutSuccessAction(SessionCommand sessionCommand, ApiContext context);
    public delegate void OnLogoutFailedAction(SessionCommand sessionCommand, ApiContext context);

    public delegate void OnGetSuccessAction<T>(T[] records, ApiResult<T> context) where T : class, IJsonObject;

    public delegate void OnSuccessAction(ApiAction context);
    public delegate void OnImageSuccessAction(string url, byte[] bytes);
    public delegate void OnFailedAction(ApiContext context);

    public delegate void OnErrorAction(ApiException exception);
}
