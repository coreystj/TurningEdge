using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace TurningEdge.Web.Windows.Helpers
{
    public static class DictionaryHelper
    {
        public static Dictionary<string, string> ToDictionary(this NameValueCollection collection)
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var key in collection.AllKeys)
            {
                dictionary.Add(key, collection[key]);
            }

            return dictionary;
        }

        public static NameValueCollection ToNameValueCollection(this Dictionary<string, string> dictionary)
        {
            var nameValueCollection = new NameValueCollection();

            foreach (var key in dictionary.Keys)
            {
                nameValueCollection.Add(key, dictionary[key]);
            }

            return nameValueCollection;
        }
    }
}
