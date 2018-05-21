using System;
using System.Collections.Generic;
using System.Text;
using TurningEdge.Math.Structs;
using TurningEdge.Modeling.Common.Graphics.DataTypes;
using TurningEdge.Modeling.Common.Graphics.Structs;

namespace TurningEdge.Modeling.Models.Meshes.Concretes
{
    [Serializable]
    public class Mesh
    {
        private string _name;
        private MaterialType _material;
        private Vector3[] _vertices;
        private Vector2[] _uvs;
        private int[] _triangles;
        private Color[] _colors;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public MaterialType Material
        {
            get { return _material; }
            set { _material = value; }
        }

        public Vector3[] Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public Vector2[] Uvs
        {
            get { return _uvs; }
            set { _uvs = value; }
        }

        public int[] Triangles
        {
            get { return _triangles; }
            set { _triangles = value; }
        }

        public Color[] Colors
        {
            get { return _colors; }
            set { _colors = value; }
        }

        public Mesh(string name, MaterialType material, List<Vector3> vertices, List<Vector2> uvs, List<int> triangles, List<Color> colors)
        : this(name, material, vertices.ToArray(), uvs.ToArray(), triangles.ToArray(), colors.ToArray()) { }

        public Mesh(string name, MaterialType material, Vector3[] vertices, Vector2[] uvs, int[] triangles, Color[] colors)
        {
            _name = name;
            _material = material;
            _vertices = vertices;
            _uvs = uvs;
            _triangles = triangles;
            _colors = colors;
        }

        public override string ToString()
        {
            return "Mesh{" +
                    "_name: " + _name + ", " +
                    "_material: " + _material + ", " +
                    "_vertices: " + _vertices.Length + ", " +
                    "_uvs: " + _uvs.Length + ", " +
                    "_triangles: " + _triangles.Length + ", " +
                    "_colors: " + _colors.Length +
                    "}";
        }
    }
}
