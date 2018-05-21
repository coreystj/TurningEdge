using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Web.Unity.MonoBehaviours;
using UnityEngine;

namespace TurningEdge.Web.Unity.Factories.Static
{
    public static class MonoWebContextFactory
    {
        public static UnityWebContext Create()
        {
            var gameObject = new GameObject();
            gameObject.name = "UnityWebContext";
            var webContext = gameObject.AddComponent<UnityWebContext>();
            return webContext;
        }
    }
}
