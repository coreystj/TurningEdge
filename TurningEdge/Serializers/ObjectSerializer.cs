using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TurningEdge.Serializers.Exceptions;

namespace TurningEdge.Serializing
{
    public static class ObjectSerializer
    {
        public static byte[] ToBytes<T>(T obj)
        {
            if (obj == null)
                throw new SerializerException("Cannot serialize a null object to a byte array.");

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        // Convert a byte array to an Object
        public static T ToObject<T>(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                throw new SerializerException("Cannot convert bytes to object due to a null or empty byte array.");

            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(bytes, 0, bytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            T obj = (T)binForm.Deserialize(memStream);

            return obj;
        }
    }
}
