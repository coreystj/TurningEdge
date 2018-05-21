using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.Generics.Factories;
using TurningEdge.IO.Interfaces;
using TurningEdge.Modeling.Models.Meshes.Concretes;

namespace TurningEdge.Modeling.Common.Factories
{
    public class ReaderFactory : Factory<IReader<Mesh>>
    {
        private static ReaderFactory _instance;

        public static ReaderFactory Instance
        {
            get {
                _instance = (_instance) ?? new ReaderFactory();
                return _instance;
            }
        }

        private IReader<Mesh> CreateReader<T>()
            where T : IReader<Mesh>
        {
            return Create(typeof(T));
        }

        public static new IReader<Mesh> Create<T>()
            where T : IReader<Mesh>
        {
            return Instance.CreateReader<T>();
        }
    }
}
