using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.Web.Exceptions;
using TurningEdge.Web.Unity.Factories.Static;
using TurningEdge.Web.WebContext.Delegates;
using TurningEdge.Web.WebContext.Interfaces;
using TurningEdge.Web.WebResult.Interfaces;
using UnityEngine;
using UnityEngine.Networking;

namespace TurningEdge.Web.Unity.MonoBehaviours
{
    public class UnityWebContext : MonoBehaviour, IWebContext
    {
        public event OnWebRequestFailedAction OnWebRequestFailed;
        public event OnWebRequestSuccessAction OnWebRequestSuccess;

        private static UnityWebContext _monoWebContext;

        public static UnityWebContext Context
        {
            get
            {
                if (_monoWebContext == null)
                    _monoWebContext = MonoWebContextFactory.Create();
                return _monoWebContext;
            }
        }

        private string _url;

        public void Get(string url)
        {
            _url = url;
            StartCoroutine(GetText());
        }

        public void Post(string url)
        {
            throw new NotImplementedException();
        }


        IEnumerator GetText()
        {
            string url = string.Empty;
            using (UnityWebRequest www = UnityWebRequest.Get(_url))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    OnWebRequestFailed(new WebContextException(www.error));
                }
                else
                {
                    // Show results as text
                    Debug.Log(www.downloadHandler.text);
                    var webRequestFactory = new Factory<IWebRequest>();
                    var webRequest = webRequestFactory.Create<
                        WebResult.Concretes.UnityWebRequest>(url, www.downloadHandler.text);

                    OnWebRequestSuccess(webRequest);
                }
            }
        }
    }
}
