using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.Helpers
{
    public static class HashSetHelper
    {
        public static void Combine<T>(this HashSet<T> list, T[] others)
        {
            foreach (var other in others)
            {
                list.Add(other);
            }
        }

        public static void Remove<T>(this HashSet<T> list, T[] others)
        {
            foreach (var other in others)
            {
                list.RemoveWhere(x => x.GetHashCode() == other.GetHashCode());
            }
        }

        public static T[] GetArray<T>(this HashSet<T> list)
        {
            var newArray = new T[list.Count];

            list.CopyTo(newArray, 0);

            return newArray;
        }
    }
}
