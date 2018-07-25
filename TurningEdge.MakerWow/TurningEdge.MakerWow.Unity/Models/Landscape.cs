using System;
using System.Collections;
using System.Collections.Generic;
using TurningEdge.Helpers;
using TurningEdge.MakerWow.Models.GameInstances;
using UnityEngine;
namespace TurningEdge.MakerWow.Unity.Models
{
    public class Landscape
    {

        private byte _size;

        private byte _layer;
        private byte _environment;
        private byte[] _states;
        private ChunkData _chunkData;

        private Vector3[] _vertices;
        private Vector3[] _normals;
        private Vector2[] _uvs;
        private byte[] _heights;
        private Color[] _colors;
        private short[] _materialIndexes;

        private Dictionary<short, List<int>> _subMeshes;

        public byte Layer
        {
            get { return _layer; }
            set { _layer = value; }
        }

        public byte Environment
        {
            get { return _environment; }
            set { _environment = value; }
        }


        public Vector3[] Vertices
        {
            get { return _vertices; }
            set
            {
                _vertices = value;
            }
        }

        public Vector3[] Normals
        {
            get { return _normals; }
        }

        public Vector2[] Uvs
        {
            get { return _uvs; }
        }

        public byte[] Heights
        {
            get { return _heights; }
            set { _heights = value; }
        }

        public Color[] Colors
        {
            get { return _colors; }
        }

        public short[] MaterialIndexes
        {
            get { return _materialIndexes; }
        }

        public byte[] States
        {
            get { return _states; }
        }

        public Dictionary<short, List<int>> SubMeshes
        {
            get { return _subMeshes; }
        }

        public ChunkData CurrentChunkData
        {
            get
            {
                GetChunkData();
                return _chunkData;
            }
            set
            {
                SetChunkData(value);
            }
        }

        public Landscape()
            : base()
        {
            _size = 15;
            Initialize();
        }

        public Landscape(ChunkData chunkData)
        : this()
        {
            CurrentChunkData = chunkData;
        }

        /// <summary>
        /// Initializes the current mesh with new details.
        /// </summary>
        public void Initialize()
        {
            int vertexCount = (_size + 1) * (_size + 1);
            _subMeshes = new Dictionary<short, List<int>>();
            _vertices = new Vector3[vertexCount];
            _normals = new Vector3[vertexCount];
            _uvs = new Vector2[vertexCount];
            _heights = new byte[vertexCount];
            _colors = new Color[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                _colors[i] = Color.white;
            }
            _materialIndexes = new short[_size * _size];
            _states = new byte[_size * _size];
        }

        /// <summary>
        /// Builds the current mesh with new parameters.
        /// </summary>
        public void Construct()
        {
            byte vertexIndex = 0;
            byte materialIndex = 0;

            _subMeshes.Clear();
            for (byte y = 0; y <= _size; y++)
            {
                for (byte x = 0; x <= _size; x++)
                {

                    vertexIndex = SetMeshIterationDetails(x, y);

                    if (x < _size && y < _size && materialIndex < _materialIndexes.Length)
                    {
                        GenerateQuad(vertexIndex, _materialIndexes[materialIndex]);
                        materialIndex++;
                    }
                }
            }
        }

        /// <summary>
        /// Sets all the mesh iteration details.
        /// </summary>
        /// <param name="x">The current x iteration.</param>
        /// <param name="y">The current y iteration.</param>
        /// <return>Returns the current vertex index.</return>
        private byte SetMeshIterationDetails(byte x, byte y)
        {
            byte vertexIndex = 0;
            byte height;
            Vector2 uvPoint;
            Vector3 vertexPosition;
            Color color;

            uvPoint = new Vector2(x, y);
            vertexIndex = GetIndexByPoint(uvPoint);

            height = _heights[vertexIndex];
            _normals[vertexIndex] = Vector3.up;
            color = _colors[vertexIndex];

            vertexPosition = new Vector3(x - 0.5f, height, y - 0.5f);

            ApplyVertexDetail(vertexIndex,
                vertexPosition,
                color,
                uvPoint);

            return vertexIndex;
        }

        /// <summary>
        /// Applies the current vertex details.
        /// </summary>
        /// <param name="index">The index you would like to edit.</param>
        /// <param name="position">The vertex height in local space.</param>
        /// <param name="color">The vertex color of the point.</param>
        public void SetVertexDetails(byte index, byte height, Color color)
        {
            Vector3 position = _vertices[index];
            ApplyVertexDetail(index,
                new Vector3(position.x, height, position.z),
                color,
                _uvs[index]);
        }

        public void SetVertexColor(byte x, byte y, Color color)
        {
            var index = GetIndexByPoint(new Vector2(x, y));
            _colors[index] = color;
        }

        public void SetVertexColor(Vector2 localPosition, Color color)
        {
            var index = GetIndexByPoint(localPosition);
            _colors[index] = color;
        }

        public void SetVertexHeight(byte x, byte y, byte height)
        {
            var index = GetIndexByPoint(new Vector2(x, y));
            _heights[index] = height;
        }

        public void SetQuadMaterial(byte x, byte y, byte materialIndex)
        {
            var index = GetQuadIndexByPoint(new Vector2(x, y));
            _materialIndexes[index] = materialIndex;
        }

        public void SetVertexHeight(Vector2 localPosition, byte height)
        {
            var index = GetIndexByPoint(localPosition);
            _heights[index] = height;
        }

        public byte GetVertexHeight(byte x, byte y)
        {
            var index = GetIndexByPoint(new Vector2(x, y));
            return _heights[index];
        }

        public byte GetVertexHeight(Vector2 localPosition)
        {
            var index = GetIndexByPoint(localPosition);
            return _heights[index];
        }

        public void SetQuadMaterial(Vector2 localPosition, byte materialIndex)
        {
            var index = GetQuadIndexByPoint(localPosition);
            _materialIndexes[index] = materialIndex;
        }

        /// <summary>
        /// Applies the current vertex point details.
        /// </summary>
        /// <param name="x">The local x coordinate of the point.</param>
        /// <param name="y">The local y coordinate of the point.</param>
        /// <param name="height">The desired new height to set.</param>
        /// <param name="color">The desired new color to set.</param>
        public void SetVertexDetails(byte x, byte y, float height, Color color)
        {
            var index = GetIndexByPoint(new Vector2(x, y));
            Vector3 position = _vertices[index];
            ApplyVertexDetail(index,
                new Vector3(position.x, height, position.z),
                color,
                _uvs[index]);
        }

        /// <summary>
        /// Applies the current vertex details.
        /// </summary>
        /// <param name="index">The index you would like to edit.</param>
        /// <param name="position">The vertex position in local space.</param>
        /// <param name="color">The vertex color of the point.</param>
        /// <param name="uv">The uv coordinate in local space.</param>
        private void ApplyVertexDetail(byte index,
            Vector3 position,
            Color color,
            Vector2 uv)
        {
            _vertices[index] = position;
            _uvs[index] = uv;
            _colors[index] = color;
        }

        /// <summary>
        /// This method converts a point in 3D space to a single value.
        /// </summary>
        /// <param name="point">The 3D point you would like to index.</param>
        /// <param name="size">The size of your grid.</param>
        /// <returns>int</returns>
        public byte GetIndexByPoint(Vector2 point)
        {
            byte index;
            index = (byte)(point.x + point.y * (_size + 1));
            return index;
        }

        public byte GetQuadIndexByPoint(Vector2 point)
        {
            byte index;
            index = (byte)(point.x + point.y * _size);
            return index;
        }

        /// <summary>
        /// Generates a quad and applies it to the designated triangle indices.
        /// </summary>
        private void GenerateQuad(byte index, short materialIndex)
        {
            int[] quadTris = new int[6];
            quadTris[0] = index;              // Bottom Left
            quadTris[1] = index + _size + 1;  // Top Left
            quadTris[2] = index + 1;          // Bottom Right

            quadTris[3] = index + _size + 1;  // Top Left
            quadTris[4] = index + _size + 2;  // Top Right
            quadTris[5] = index + 1;          // Bottom Right

            AddSubmeshQuad(materialIndex, quadTris);
        }

        private void AddSubmeshQuad(short materialIndex, int[] indices)
        {
            List<int> subMesh;
            if (_subMeshes.TryGetValue(materialIndex, out subMesh))
            {
                subMesh.AddRange(indices);
            }
            else
            {
                subMesh = new List<int>(indices);
                _subMeshes.Add(materialIndex, subMesh);
            }
        }

        private void SetChunkData(ChunkData chunkData)
        {
            int index = 0;
            int secondaryIndex = 0;
            float color;
            for (int y = 0; y <= _size; y++)
            {
                for (int x = 0; x <= _size; x++)
                {

                    ActionHelper.DoAction(() =>
                    {
                        _heights[index] = chunkData.Heights[index];
                    });
                    ActionHelper.DoAction(() =>
                    {
                        color = (chunkData.Colors[index] / 100f);
                        _colors[index] = new Color(color, color, color, 1f);
                    });
                    if (x < _size && y < _size)
                    {
                        ActionHelper.DoAction(() =>
                        {
                            _materialIndexes[secondaryIndex] = chunkData.Materials[secondaryIndex];
                        });
                        ActionHelper.DoAction(() =>
                        {
                            _states[secondaryIndex] = chunkData.States[secondaryIndex];
                        });
                        secondaryIndex++;
                    }
                    index++;
                }
            }

            _chunkData = chunkData;
        }

        private ChunkData GetChunkData()
        {

            int index = 0;
            int secondaryIndex = 0;
            for (int y = 0; y <= _size; y++)
            {
                for (int x = 0; x <= _size; x++)
                {
                    if (x < _size && y < _size)
                    {
                        ActionHelper.DoAction(() =>
                        {
                            _chunkData.Heights[secondaryIndex] = _heights[secondaryIndex];
                        });
                        ActionHelper.DoAction(() =>
                        {
                            _chunkData.Colors[secondaryIndex] = (byte)(_colors[secondaryIndex].r * 100);
                        });
                        secondaryIndex++;
                    }
                    ActionHelper.DoAction(() =>
                    {
                        _chunkData.Materials[index] = _materialIndexes[index];
                    });
                    ActionHelper.DoAction(() =>
                    {
                        _chunkData.States[index] = _states[index];
                    });
                    index++;
                }
            }

            return _chunkData;
        }
    }
}