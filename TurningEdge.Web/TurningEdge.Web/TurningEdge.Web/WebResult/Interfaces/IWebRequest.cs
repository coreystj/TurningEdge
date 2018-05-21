using System;
using System.Collections.Generic;
using System.Text;

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
        
    }
}
