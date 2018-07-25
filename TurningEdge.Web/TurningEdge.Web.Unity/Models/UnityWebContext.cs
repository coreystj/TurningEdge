using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.Serializers;
using TurningEdge.Web.Exceptions;
using TurningEdge.Web.Unity.Factories.Static;
using TurningEdge.Web.Unity.Helpers;
using TurningEdge.Web.Unity.Models;
using TurningEdge.Web.Unity.MonoBehaviours;
using TurningEdge.Web.WebContext.Delegates;
using TurningEdge.Web.WebContext.Interfaces;
using TurningEdge.Web.WebResult.Interfaces;
using UnityEngine;
using UnityEngine.Networking;

namespace TurningEdge.Web.Unity.Models
{
    public class UnityWebContext : IWebContext
    {
        private static Dictionary<string, RequestContainer> _requests;

        public UnityWebContext()
        {
            _requests = new Dictionary<string, RequestContainer>();
        }

        //private string _url;

        public void Post(Dictionary<string, string> formData,
            string url,
            OnWebRequestSuccessAction successAction,
            OnWebRequestFailedAction failedAction = null)
        {
            WWWForm form = formData.ToWWWForm();
            var container = new RequestContainer(successAction, failedAction, form);
            MonoUnityWebContext.Context.StartCoroutine(PostText(url, container));
        }

        IEnumerator GetImageText(string url, RequestContainer details)
        {
            //string url = string.Empty;
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();
                //RequestContainer details = _requests[www.url];
                if (www.isNetworkError || www.isHttpError)
                {
                    //OnWebRequestFailed(new WebContextException(www.error));
                    details.OnFailed(new WebContextException(www.error));
                }
                else
                {
                    // Show results as text
                    string rawData = www.downloadHandler.text;
                    var webRequestFactory = new Factory<IWebRequest>();

                    Texture2D tex = DownloadHandlerTexture.GetContent(www);
                    byte[] bytes = tex.GetRawTextureData();
                    string base64Encoded = bytes.Encode();

                    var webRequest = webRequestFactory.Create<
                        WebResult.Concretes.UnityWebRequest>(base64Encoded, www.url);


                    details.OnSuccess(webRequest);
                }
            }
        }

        IEnumerator GetText(string url, RequestContainer details)
        {
            //string url = string.Empty;
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();
                //RequestContainer details = _requests[url];
                if (www.isNetworkError || www.isHttpError)
                {
                    //OnWebRequestFailed(new WebContextException(www.error));
                    details.OnFailed(new WebContextException(www.error));
                }
                else
                {
                    // Show results as text
                    string rawData = www.downloadHandler.text;
                    var webRequestFactory = new Factory<IWebRequest>();
                    var webRequest = webRequestFactory.Create<
                        WebResult.Concretes.UnityWebRequest>(rawData, www.url);

                    details.OnSuccess(webRequest);
                }
            }
        }

        IEnumerator PostText(string url, RequestContainer details)
        {
            //string url = string.Empty;
            //RequestContainer details = _requests[url];

            using (UnityWebRequest www = UnityWebRequest.Post(url, details.Form))
            {
                yield return www.SendWebRequest();
                
                if (www.isNetworkError || www.isHttpError)
                {
                    //OnWebRequestFailed(new WebContextException(www.error));
                    details.OnFailed(new WebContextException(www.error));
                }
                else
                {
                    // Show results as text
                    //Debug.Log(www.downloadHandler.text);
                    string rawData = www.downloadHandler.text;

                    var webRequestFactory = new Factory<IWebRequest>();
                    var webRequest = webRequestFactory.Create<
                        WebResult.Concretes.UnityWebRequest>(rawData, www.url);

                        Debug.Log(webRequest.RawData);
                    details.OnSuccess(webRequest);
                }
            }
        }

        public void Get(string url, OnWebRequestSuccessAction successAction, OnWebRequestFailedAction failedAction = null)
        {
            //_url = url;
            var container = new RequestContainer(successAction, failedAction);
            MonoUnityWebContext.Context.StartCoroutine(GetText(url, container));
        }

        public void GetImage(string url, OnWebRequestSuccessAction successAction, OnWebRequestFailedAction failedAction = null)
        {
            var container = new RequestContainer(successAction, failedAction);
            MonoUnityWebContext.Context.StartCoroutine(GetImageText(url, container));
        }
    }
}
