using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Debugging;

namespace TurningEdge.Unity.Helpers
{
    public static class DebugHelper
    {
        public static void Print(params object[] objects)
        {
            UnityEngine.Debug.Log(ObjectsToString(objects));
            Debugger.Print(objects);
        }

        public static void PrintWarning(params object[] objects)
        {
            UnityEngine.Debug.Log(ObjectsToString(objects));
            Debugger.PrintWarning(objects);
        }

        public static void PrintError(Exception e)
        {
            UnityEngine.Debug.Log(e + "");
            Debugger.PrintError(e);
        }

        private static string ObjectsToString(params object[] objects)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string delimiter = "";

            foreach (var obj in objects)
            {
                stringBuilder.Append(delimiter);
                stringBuilder.Append(obj);

                delimiter = ", ";
            }

            return stringBuilder.ToString();
        }
    }
}
