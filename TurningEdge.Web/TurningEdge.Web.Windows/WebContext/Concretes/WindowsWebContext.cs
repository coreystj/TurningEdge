﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.Serializing;
using TurningEdge.Web.Helpers;
using TurningEdge.Web.WebContext.Delegates;
using TurningEdge.Web.WebContext.Interfaces;
using TurningEdge.Web.WebResult.Interfaces;
using TurningEdge.Web.Windows.Helpers;
using TurningEdge.Web.Windows.WebResult.Concretes;

namespace TurningEdge.Web.Windows.WebContext.Concretes
{
    public class WindowsWebContext : IWebContext
    {
        private BReq _req;

        public WindowsWebContext()
        {
            _req = new BReq();
        }

        public void Get(string url, 
            OnWebRequestSuccessAction successAction, 
            OnWebRequestFailedAction failedAction)
        {
            SendGetRequest(url, successAction, failedAction);
        }

        public void GetImage(string url, OnWebRequestSuccessAction successAction, OnWebRequestFailedAction failedAction = null)
        {
            throw new NotImplementedException();
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
            var webRequestFactory = new Factory<IWebRequest>();
            string rawData = _req.HttpGet(url);
            var webRequest = webRequestFactory.Create<WindowsWebRequest>(rawData, url);

            successAction(webRequest);
        }


        private void SendPostRequest(Dictionary<string, string> formData,
            string url,
            OnWebRequestSuccessAction successAction,
            OnWebRequestFailedAction failedAction)
        {
            var webRequestFactory = new Factory<IWebRequest>();
            string rawData = _req.HttpPost(url, formData.FormSerialize());
            var webRequest = webRequestFactory.Create<WindowsWebRequest>(rawData, url);

            successAction(webRequest);
        }
    }

    /// <summary>
    /// A simple basic class for HTTP Requests.
    /// </summary>
    public class BReq
    {
        /// <summary>
        /// UserAgent to be used on the requests
        /// </summary>
        public string UserAgent = @"Mozilla/5.0 (Windows; Windows NT 6.1) AppleWebKit/534.23 (KHTML, like Gecko) Chrome/11.0.686.3 Safari/534.23";

        /// <summary>
        /// Cookie Container that will handle all the cookies.
        /// </summary>
        private CookieContainer cJar;

        /// <summary>
        /// Performs a basic HTTP GET request.
        /// </summary>
        /// <param name="url">The URL of the request.</param>
        /// <returns>HTML Content of the response.</returns>
        public string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cJar;
            request.UserAgent = UserAgent;
            request.KeepAlive = false;
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Performs a basic HTTP POST request
        /// </summary>
        /// <param name="url">The URL of the request.</param>
        /// <param name="post">POST Data to be passed.</param>
        /// <param name="refer">Referrer of the request</param>
        /// <returns>HTML Content of the response.</returns>
        public string HttpPost(string url, string post, string refer = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cJar;
            request.UserAgent = UserAgent;
            request.KeepAlive = false;
            request.Method = "POST";
            request.Referer = refer;

            byte[] postBytes = Encoding.ASCII.GetBytes(post);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());

            return sr.ReadToEnd();
        }

        ///// <summary>
        ///// Gets the image from the response stream.
        ///// </summary>
        ///// <param name="imageUrl">The image URL.</param>
        ///// <returns>Image type of the image</returns>
        //public Image GetImage(string imageUrl)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
        //    request.CookieContainer = cJar;
        //    request.UserAgent = UserAgent;
        //    request.KeepAlive = false;
        //    request.Method = "GET";
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    return Image.FromStream(response.GetResponseStream());
        //}

        /// <summary>
        /// Creates an HTML file from the string.
        /// </summary>
        /// <param name="html">HTML String.</param>
        public void DebugHtml(string html)
        {
            StreamWriter sw = new StreamWriter("debug.html");
            sw.Write(html);
            sw.Close();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BReq"/> class.
        /// </summary>
        public BReq()
        {
            cJar = new CookieContainer();
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="BReq"/> is reclaimed by garbage collection.
        /// </summary>
        ~BReq()
        {
            // Nothing here
        }
    }
}
