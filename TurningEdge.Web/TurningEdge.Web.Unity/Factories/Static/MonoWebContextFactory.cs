using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Web.Unity.Models;
using TurningEdge.Web.Unity.MonoBehaviours;
using UnityEngine;

namespace TurningEdge.Web.Unity.Factories.Static
{
    public static class MonoWebContextFactory
    {
        public static MonoUnityWebContext CreateMono()
        {
            var gameObject = new GameObject();
            gameObject.name = "MonoUnityWebContext";
            var webContext = gameObject.AddComponent<MonoUnityWebContext>();
            return webContext;
        }

        public static UnityWebContext Create()
        {
            return new UnityWebContext();
        }
    }
}
