using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.Web.Exceptions;
using TurningEdge.Web.WebContext.Interfaces;
using TurningEdge.Web.WebResult.Interfaces;
using TurningEdge.Web.Windows.WebContext.Concretes;

namespace TurningEdge.Web.Pipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            //string url = "http://www.Google.ca";

            //var factory = new Factory<IWebContext>();
            //var webContext = factory.Create<WindowsWebContext>();

            //webContext.OnWebRequestSuccess += WebContext_OnWebRequestSuccess;
            //webContext.OnWebRequestFailed += WebContext_OnWebRequestFailed;

            //webContext.Get(url);

        }

        private static void WebContext_OnWebRequestFailed(WebContextException ex)
        {
            throw new NotImplementedException();
        }

        private static void WebContext_OnWebRequestSuccess(IWebRequest result)
        {
            Console.WriteLine("Received data successfully from: " + result.Url 
                + " with data: " + result.RawData);
        }
    }
}
