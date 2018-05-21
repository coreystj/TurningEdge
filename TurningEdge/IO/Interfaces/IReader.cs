using System;
using System.Collections.Generic;
using System.Text;

namespace TurningEdge.IO.Interfaces
{
    public interface IReader<T>
    {
        T Read(string path);
        string Write(T model, string path);
        T Deserialize(byte[] bytes);
        byte[] Serialize(T model);
    }
}
