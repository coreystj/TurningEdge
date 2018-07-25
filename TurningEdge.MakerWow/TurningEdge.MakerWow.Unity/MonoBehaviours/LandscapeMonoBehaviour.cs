using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Models.GameInstances;
using TurningEdge.MakerWow.Unity.Models;
using TurningEdge.MakerWow.Unity.Repositories;
using UnityEngine;

namespace TurningEdge.MakerWow.Unity.MonoBehaviours
{
    public class LandscapeMonoBehaviour : MonoBehaviour
    {
        private Landscape _landscape;
        private Shader _shader;
        public Material[] Materials;

        public void SetChunkData(ChunkData chunkData)
        {
            _landscape = new Landscape(chunkData);
        }

        //private Material[] _materials;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private MeshCollider _meshCollider;

        // Use this for initialization
        public void Initialize(Shader shader)
        {
            _landscape.Construct();
            _shader = shader;

            _meshFilter = GetComponent<MeshFilter>();
            if (_meshFilter == null)
                _meshFilter = gameObject.AddComponent<MeshFilter>();

            _meshRenderer = GetComponent<MeshRenderer>();
            if (_meshRenderer == null)
                _meshRenderer = gameObject.AddComponent<MeshRenderer>();

            _meshCollider = GetComponent<MeshCollider>();
            if (_meshCollider == null)
                _meshCollider = gameObject.AddComponent<MeshCollider>();

            _meshFilter.sharedMesh = new Mesh();
            _meshCollider.sharedMesh = _meshFilter.sharedMesh;

            //transform.gameObject.layer = LayerMask.NameToLayer("Landscape");

            ApplyTopology();
            ApplyMaterials();
        }

        /// <summary>
        /// Visually updates the landscape topology.
        /// </summary>
        public void ApplyTopology()
        {
            _meshFilter.sharedMesh.vertices = _landscape.Vertices;
            _meshFilter.sharedMesh.uv = _landscape.Uvs;
            _meshFilter.sharedMesh.colors = _landscape.Colors;

            _meshFilter.sharedMesh.normals = _landscape.Normals;
        }

        /// <summary>
        /// Visually updates all materials.
        /// </summary>
        public void ApplyMaterials()
        {
            List<int> subMeshIndices;
            byte submeshIndex = 0;
            Material[] materials = new Material[_landscape.SubMeshes.Count];

            _meshFilter.sharedMesh.subMeshCount = _landscape.SubMeshes.Count;
            foreach (byte materialIndex in _landscape.SubMeshes.Keys)
            {
                subMeshIndices = _landscape.SubMeshes[materialIndex];
                
                materials[submeshIndex] = GroundMaterialRepository.GetGroundMaterial(
                    materialIndex, _shader);
                _meshFilter.sharedMesh.SetTriangles(subMeshIndices, submeshIndex);

                submeshIndex++;
            }

            _meshRenderer.sharedMaterials = materials;

            _meshCollider.sharedMesh = null;
            _meshCollider.sharedMesh = _meshFilter.sharedMesh;
        }
    }
}
