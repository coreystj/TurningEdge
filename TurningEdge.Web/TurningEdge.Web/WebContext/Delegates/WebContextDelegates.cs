using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TurningEdge.Web.Exceptions;
using TurningEdge.Web.WebResult.Interfaces;

namespace TurningEdge.Web.WebContext.Delegates
{
    public delegate void OnWebRequestSuccessAction(IWebRequest result);
    public delegate void OnWebRequestFailedAction(WebContextException error);
}
