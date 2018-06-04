using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.Web.WebContext.Delegates;
using TurningEdge.Web.WebContext.Interfaces;
using TurningEdge.Web.WebResult.Interfaces;
using TurningEdge.Web.Windows.WebResult.Concretes;

namespace TurningEdge.Web.Windows.WebContext.Concretes
{
    public class WindowsWebContext : IWebContext
    {

        public void Get(string url, 
            OnWebRequestSuccessAction successAction, 
            OnWebRequestFailedAction failedAction)
        {
            SendRequest(url, successAction, failedAction);
        }

        public void Post(string url,
            OnWebRequestSuccessAction successAction,
            OnWebRequestFailedAction failedAction)
        {
            SendRequest(url, successAction, failedAction);
        }

        private void SendRequest(string url,
            OnWebRequestSuccessAction successAction,
            OnWebRequestFailedAction failedAction)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            var webRequestFactory = new Factory<IWebRequest>();

            string rawData = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var webRequest = webRequestFactory.Create<WindowsWebRequest>(rawData, url);

            successAction(webRequest);
         }
    }
}
