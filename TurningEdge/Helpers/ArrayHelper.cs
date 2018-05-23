using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.Helpers
{
    public static class ArrayHelper
    {
        public static List<T[]> Split<T>(this T[] arr, int n)
        {
            List<T[]> splitted = new List<T[]>();//This list will contain all the splitted arrays.

            int arrayLength = arr.Length;

            for (int i = 0; i < arrayLength; i = i + n)
            {
                T[] val;

                if (arrayLength < i + n)
                {
                    n = arrayLength - i;
                    val = new T[n];
                }
                else
                    val = new T[n];
                Array.Copy(arr, i, val, 0, n);
                splitted.Add(val);
            }

            return splitted;
        }
    }
}
