using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Delegates;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Api.Repositories.Abstracts;

namespace TurningEdge.MakerWow.Api.Repositories
{
    public class InventoryRepository : ApiRepository<InventoryJsonObject>
    {
        protected override string SetPrimaryData(List<string> primaryKeys)
        {
            primaryKeys.Add("id");
            primaryKeys.Add("user_id");

            return "inventory";
        }
    }
}
