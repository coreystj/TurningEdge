using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Api.Models.Interfaces
{
    public interface IJsonObject
    {
        void ParseJson(Dictionary<string, object> record);
        string SerializeJson();
    }
}
