using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.Web.Helpers
{
    public static class DictionaryHelper
    {
        public static string FormSerialize(this Dictionary<string, string> dictionary)
        {
            var nameValueCollection = new StringBuilder();
            string concat = "";
            foreach (var key in dictionary.Keys)
            {
                nameValueCollection.Append(concat);
                nameValueCollection.Append(key);
                nameValueCollection.Append("=");
                nameValueCollection.Append(dictionary[key]);

                concat = "&";
            }

            return nameValueCollection.ToString();
        }
    }
}
