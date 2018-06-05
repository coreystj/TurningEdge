using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Models;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.Web.Models;

namespace TurningEdge.MakerWow.Api.Helpers
{
    public static class ApiExceptionHelper
    {
        public static ApiException ToError(this object result)
        {
            var errorData = result as Dictionary<string, object>;
            int id = (int)errorData["id"];
            return new ApiException(id, (string)errorData["message"]);
        }
    }
}
