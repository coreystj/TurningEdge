using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Debugging;
using TurningEdge.MakerWow.Api.Managers;
using TurningEdge.MakerWow.Api.Models;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Models.GameInstances;
using UnityEngine;

namespace TurningEdge.MakerWow.Unity.Repositories
{
    public static class GroundMaterialRepository
    {
        private static Dictionary<int, Material> _materials;

        static GroundMaterialRepository()
        {
            _materials = new Dictionary<int, Material>();
        }

        public static Material GetGroundMaterial(int id, Shader shader)
        {
            Material material = null;
            if (!_materials.TryGetValue(id, out material))
            {
                MakerWOWApi.GroundRepository.Read(id, onReadSuccessAction, onReadFailedAction);
                material = new Material(shader);
                material.SetColor("_ShoreColor", Color.white);
                material.SetColor("_WaterColor", new Color(0, 0.7372f, 1f));
                material.SetColor("_SkyColor", Color.white);
                _materials[id] = material;
            }
            return material;
        }

        private static void onReadFailedAction(ApiContext context)
        {
            Debugger.PrintError(context.Error);
        }

        private static void onReadSuccessAction(
            GroundJsonObject[] records, ApiResult<GroundJsonObject> context)
        {
            Material material;

            if(records.Length > 0)
            {
                Ground ground = records[0];
                material = _materials[ground.Id];

                ImageRepository.GetImage(ground.Icon, 
                    (string url, Texture2D texture)=> {
                    material.mainTexture = texture;
                });

                
            }
        }
    }
}
