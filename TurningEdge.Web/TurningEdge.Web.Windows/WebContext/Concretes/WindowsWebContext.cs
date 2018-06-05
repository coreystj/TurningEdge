using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.Web.WebContext.Delegates;
using TurningEdge.Web.WebContext.Interfaces;
using TurningEdge.Web.WebResult.Interfaces;
using TurningEdge.Web.Windows.Helpers;
using TurningEdge.Web.Windows.WebResult.Concretes;

namespace TurningEdge.Web.Windows.WebContext.Concretes
{
    public class WindowsWebContext : IWebContext
    {

        public void Get(string url, 
            OnWebRequestSuccessAction successAction, 
            OnWebRequestFailedAction failedAction)
        {
            SendGetRequest(url, successAction, failedAction);
        }

        public void Post(Dictionary<string, string> formData, 
            string url,
            OnWebRequestSuccessAction successAction,
            OnWebRequestFailedAction failedAction)
        {
            SendPostRequest(formData, url, successAction, failedAction);
        }

        private void SendGetRequest(string url,
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

        private void SendPostRequest(Dictionary<string, string> formData,
            string url,
            OnWebRequestSuccessAction successAction,
            OnWebRequestFailedAction failedAction)
        {
            string responseInString = string.Empty;

            using (var wb = new WebClient())
            {
                var response = wb.UploadValues(url, "POST", formData.ToNameValueCollection());
                responseInString = Encoding.UTF8.GetString(response);
            }

            var webRequestFactory = new Factory<IWebRequest>();
            var webRequest = webRequestFactory.Create<WindowsWebRequest>(responseInString, url);

            successAction(webRequest);
        }
    }
}
