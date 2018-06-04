using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.Web.Models;

namespace TurningEdge.Web.WebResult.Interfaces
{
    public interface IWebRequest
    {
        string Url
        {
            get;
        }

        string RawData
        {
            get;
        }

        JsonResult Json
        {
            get;
        }

    }
}
