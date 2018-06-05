using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.Web.WebContext.Delegates;
using TurningEdge.Web.WebContext.Interfaces;
using TurningEdge.Web.WebResult.Interfaces;

namespace TurningEdge.MakerWow.Api.Windows
{
    public abstract class Context<T>
        where T : IWebContext
    {
        protected IWebContext _webContext;

        public Context()
        {

        }

        public abstract void GetWorldData(
            OnWebRequestSuccessAction successAction, OnWebRequestFailedAction failedAction);

        public abstract void SetWorldData(
            ChunkData[] worldDatas, OnWebRequestSuccessAction successAction, 
            OnWebRequestFailedAction failedAction);
    }
}
