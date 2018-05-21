using System;
using System.Collections.Generic;
using TurningEdge.Generics.Abstracts;

namespace TurningEdge.Generics.Interfaces
{
    public  interface IFactory<T>
    {
        T Create<Y>() where Y : T;
        T Create(Type type);
        T Create(Type type, params object[] args);
    }
}
