using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace TurningEdge.Web.Unity.Helpers
{
    public static class DictionaryHelper
    {
        public static WWWForm ToWWWForm(this Dictionary<string, string> dictionary)
        {
            var nameValueCollection = new WWWForm();

            foreach (var key in dictionary.Keys)
            {
                nameValueCollection.AddField(key, dictionary[key]);
            }

            return nameValueCollection;
        }
    }
}
