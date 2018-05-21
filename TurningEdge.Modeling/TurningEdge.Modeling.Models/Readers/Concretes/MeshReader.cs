using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TurningEdge.IO.Interfaces;
using TurningEdge.Modeling.Models.Meshes.Concretes;
using TurningEdge.Serializers;
using TurningEdge.Serializing;

namespace TurningEdge.Modeling.Models.Readers.Concretes
{
    public class MeshReader : IReader<Mesh>
    {
        public const string EXTENSION = ".tem";

        public Mesh Deserialize(byte[] bytes)
        {
            return ObjectSerializer.ToObject<Mesh>(bytes);
        }

        public Mesh Read(string path)
        {
            return Deserialize(Base64Serializer.Decode(File.ReadAllText(path)));
        }

        public byte[] Serialize(Mesh mesh)
        {
            return ObjectSerializer.ToBytes(mesh);
        }

        public string Write(Mesh mesh, string path)
        {
            string fileName = path + "/" + mesh.Name + EXTENSION;
            File.WriteAllText(fileName, Base64Serializer.Encode(Serialize(mesh)));
            return fileName;
        }
    }
}
