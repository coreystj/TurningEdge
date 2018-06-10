using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Api.Repositories.Abstracts;

namespace TurningEdge.MakerWow.Api.Repositories
{
    public class StockpileRepository : ApiRepository<StockpileJsonObject>
    {
        protected override string SetPrimaryData(List<string> primaryKeys)
        {
            primaryKeys.Add("id");
            primaryKeys.Add("user_id");

            return "stockpile";
        }
    }
}
