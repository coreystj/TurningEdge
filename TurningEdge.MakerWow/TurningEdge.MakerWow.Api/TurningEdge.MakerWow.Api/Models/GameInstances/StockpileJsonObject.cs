using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Api.Models.GameInstances
{
    [Serializable]
    public class StockpileJsonObject : InventoryJsonObject
    {
        public StockpileJsonObject(object record) 
            : base(record)
        {
        }

        public StockpileJsonObject(int id, int userId, int slotCount) 
            : base(id, userId, slotCount)
        {
        }

        public StockpileJsonObject(int id, int userId, string data) 
            : base(id, userId, data)
        {
        }
    }
}
