using System;
using System.Collections.Generic;
using System.Text;

namespace TurningEdge.MakerWow.Models
{
    public class MakerWowEngine
    {
        public MakerWowEngine()
        {
            TurningEdge.Web.Unity.Factories.Static.MonoWebContextFactory.Create();
        }
    }
}
