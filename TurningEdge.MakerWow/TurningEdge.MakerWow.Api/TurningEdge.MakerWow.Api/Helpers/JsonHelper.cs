using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurningEdge.MakerWow.Api.Helpers
{
    public static class JsonHelper
    {
        public static T[] FromFormat<T>(this string rawData)
        {

            string[] dataLines = rawData.Split('|');
            T[] result = new T[15*15];
            int i = 0;
            try
            {
                foreach (var data in dataLines)
                {
                    result[i] = (T)Convert.ChangeType(int.Parse(data), typeof(T));
                    i++;
                }
            }
            catch(Exception)
            { }

            return result;
        }

        public static string ToFormat<T>(this T[] data)
        {
            var result = new StringBuilder();

            int i = 0;
            foreach (var number in data)
            {
                result.Append(number.ToString());
                if(i < data.Length - 1)
                    result.Append("|");
                i++;
            }

            return result.ToString();
        }
    }
}
