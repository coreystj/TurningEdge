using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Delegates;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Api.Repositories.Abstracts;

namespace TurningEdge.MakerWow.Api.Repositories
{
    public class GroundRepository : ApiRepository<GroundJsonObject>
    {
        protected override string SetPrimaryData(List<string> primaryKeys)
        {
            primaryKeys.Add("id");

            return "ground";
        }

        public void Read(int id,
            OnGetSuccessAction<GroundJsonObject> onReadSuccessAction,
            OnFailedAction onReadFailedAction)
        {
            var filter = new Dictionary<string, string>();
            filter.Add("id", id.ToString());

            Read(onReadSuccessAction, onReadFailedAction, filter);
        }
    }
}


