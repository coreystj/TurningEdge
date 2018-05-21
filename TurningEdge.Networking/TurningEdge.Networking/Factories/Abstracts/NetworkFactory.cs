using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.Networking.Models.Abstracts;

namespace TurningEdge.Networking.Factories.Abstracts
{
    public class NetworkFactory<T> : Factory<T>
        where T : NetworkInfo
    {
        public NetworkFactory(params object[] initializers)
            : base(initializers)
        {

        }
    }
}
