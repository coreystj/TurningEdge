using System;
using System.Collections.Generic;
using TurningEdge.Generics.Interfaces;

namespace TurningEdge.Generics.Factories
{
    public class Factory<T> : IFactory<T>
    {
        private object[] _initializers;

        public Factory()
        {
            _initializers = new object[0];
        }

        public Factory(params object[] initializers)
        {
            _initializers = initializers;
        }

        public T Create(Type type)
        {
            return Create(type, _initializers);
        }

        public T Create(Type type, params object[] args)
        {
            return (T)Activator.CreateInstance(type, args);
        }

        public T Create<Y>()
            where Y : T
        {
            return Create(typeof(Y), _initializers);
        }

        public T Create<Y>(params object[] args)
            where Y : T
        {
            return Create(typeof(Y), args);
        }
    }
}
