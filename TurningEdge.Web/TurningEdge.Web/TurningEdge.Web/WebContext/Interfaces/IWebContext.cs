using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.Web.WebContext.Delegates;

namespace TurningEdge.Web.WebContext.Interfaces
{
    public interface IWebContext
    {
        void Get(string url, OnWebRequestSuccessAction successAction, OnWebRequestFailedAction failedAction = null);
        void Post(string url, OnWebRequestSuccessAction successAction, OnWebRequestFailedAction failedAction = null);
    }
}
