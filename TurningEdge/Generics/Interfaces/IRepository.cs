using System;
using System.Collections.Generic;

namespace TurningEdge.Generics.Interfaces
{
    public interface IRepository<T>
    {
        T Create(T model);
        T Read(T model);
        T Update(T model);
        T Delete(T model);
    }
}
