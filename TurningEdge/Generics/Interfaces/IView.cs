using System;
using System.Collections.Generic;
using TurningEdge.Generics.Abstracts;

namespace TurningEdge.Generics.Interfaces
{
    public interface IView<T>
    {
        void Update(T model);

        T Read();
    }
}
