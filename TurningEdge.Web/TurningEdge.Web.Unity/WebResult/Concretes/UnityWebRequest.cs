using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.Web.Models;
using TurningEdge.Web.WebResult.Interfaces;

namespace TurningEdge.Web.Unity.WebResult.Concretes
{
    public class UnityWebRequest : IWebRequest
    {
        private string _rawData;
        private string _url;

        public string RawData
        {
            get
            {
                return _rawData;
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
        }

        public JsonResult Json
        {
            get
            {
                return new JsonResult(_rawData);
            }
        }

        public UnityWebRequest(string rawData, string url)
        {
            _url = url;
            _rawData = rawData;
        }
    }
}
