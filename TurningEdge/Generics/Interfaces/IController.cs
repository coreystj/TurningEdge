using System;
using System.Collections.Generic;

namespace TurningEdge.Generics.Interfaces
{
    public interface IController<T>
    {
        void Bind(T model);

        T Unbind();
    }
}
