using System;
using System.Collections.Generic;
using System.Text;

namespace TurningEdge.Helpers
{
    public static class EnumHelper
    {
        public static T ParseEnum<T>(this string enumString)
            where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), enumString, true);
        }

        public static object ParseEnum(this string enumString, Type propType)
        {
            return Enum.Parse(propType, enumString, true);
        }
    }
}
