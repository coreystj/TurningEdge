using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Delegates;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Api.Models.Relationships;
using TurningEdge.MakerWow.Api.Repositories.Abstracts;

namespace TurningEdge.MakerWow.Api.Repositories
{
    public class RelationshipSkillUserRepository : ApiRepository<RelationshipSkillUserJsonObject>
    {
        protected override string SetPrimaryData(List<string> primaryKeys)
        {
            primaryKeys.Add("user_id");
            primaryKeys.Add("skill_id");

            return "rel_skill_user";
        }
    }
}
