using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Models;

namespace TurningEdge.MakerWow.Api.Helpers
{
    public static class UserHelper
    {
        public static User ToUser(this object result)
        {
            var userData = result as Dictionary<string, object>;

            return null;
        }
    }
}
