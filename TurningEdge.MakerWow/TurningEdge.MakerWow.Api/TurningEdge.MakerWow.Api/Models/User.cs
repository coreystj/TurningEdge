using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models.Abstracts;

namespace TurningEdge.MakerWow.Api.Models
{
    public class User : JsonObject
    {
        public User(object record) 
            : base(record)
        {
        }

        protected override void ParseJson(object record)
        {
            throw new NotImplementedException();
        }
    }
}
