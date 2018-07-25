using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Debugging;
using TurningEdge.MakerWow.Api.Managers;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.Web.Unity.MonoBehaviours;
using UnityEngine;
using UnityEngine.Networking;

namespace TurningEdge.MakerWow.Unity.Repositories
{
    public static class ImageRepository
    {
        private static Dictionary<string, Texture2D> _images;

        static ImageRepository()
        {
            _images = new Dictionary<string, Texture2D>();
        }

        public static void GetImage(string url, Action<string,Texture2D> action)
        {

            MonoUnityWebContext.Context.StartCoroutine(GetImageText(MakerWOWApi.DOMAIN_NAME + url, action));
        }

        private static IEnumerator GetImageText(string url, Action<string, Texture2D> action)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debugger.PrintError(new Exception(www.error));
                }
                else
                {
                    Texture2D tex = DownloadHandlerTexture.GetContent(www);
                    action(url, tex);
                }
            }
        }
    }
}
