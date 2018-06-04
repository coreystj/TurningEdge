using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.Web.Models;

namespace TurningEdge.MakerWow.Api.Helpers
{
    public static class WorldDataHelper
    {
        public static WorldData[] ToWorldData(this ApiResult<WorldData> result)
        {


            WorldData[] worldData = null;

            if (worldData == null)
                worldData = new WorldData[0];

            return worldData;
        }
    }
}
