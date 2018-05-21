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
        public event OnWebRequestFailedAction OnWebRequestFailed = delegate { };
        public event OnWebRequestSuccessAction OnWebRequestSuccess = delegate { };

        public void Get(string url)
        {
            SendRequest(url);
        }

        public void Post(string url)
        {
            SendRequest(url);
        }

        private void SendRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            var webRequestFactory = new Factory<IWebRequest>();

            string rawData = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var webRequest = webRequestFactory.Create<WindowsWebRequest>(url, rawData);

            OnWebRequestSuccess(webRequest);
         }
    }
}
