using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.Web.WebContext.Delegates;

namespace TurningEdge.Web.WebContext.Interfaces
{
    public interface IWebContext
    {
        event OnWebRequestSuccessAction OnWebRequestSuccess;
        event OnWebRequestFailedAction OnWebRequestFailed;

        void Get(string url);
        void Post(string url);
    }
}
