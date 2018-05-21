using System;
using System.Collections.Generic;
using TurningEdge.Generics.Interfaces;

namespace TurningEdge.Generics.Factories
{
    public class Factory<T> : IFactory<T>
    {
        public T Create(Type type)
        {
            return Create(type, new object[0]);
        }

        public T Create(Type type, params object[] args)
        {
            return (T)Activator.CreateInstance(type, args);
        }

        public T Create<Y>()
            where Y : T
        {
            return Create(typeof(Y));
        }

        public T Create<Y>(params object[] args)
            where Y : T
        {
            return Create(typeof(Y), args);
        }
    }
}
