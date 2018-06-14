using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Web.Unity.Factories.Static;
using UnityEngine;

namespace TurningEdge.Web.Unity.MonoBehaviours
{
    public class MonoUnityWebContext : MonoBehaviour
    {

        private static MonoUnityWebContext _monoWebContext;

        public static MonoUnityWebContext Context
        {
            get
            {
                if (_monoWebContext == null)
                    _monoWebContext = MonoWebContextFactory.CreateMono();
                return _monoWebContext;
            }
        }
    }
}
