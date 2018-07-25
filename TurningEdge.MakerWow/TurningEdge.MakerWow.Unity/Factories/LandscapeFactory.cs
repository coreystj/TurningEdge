using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Models.GameInstances;
using TurningEdge.MakerWow.Unity.MonoBehaviours;
using UnityEngine;

namespace TurningEdge.MakerWow.Unity.Factories
{
    public static class LandscapeFactory
    {
        public static GameObject Create(ChunkData chunkData, Shader shader)
        {
            var gameObject = new GameObject();
            var test = gameObject.AddComponent<LandscapeMonoBehaviour>();

            test.SetChunkData(chunkData);
            test.Initialize(shader);
            gameObject.name = chunkData.X + ", " + chunkData.Y;
            return gameObject;
        }
    }
}
