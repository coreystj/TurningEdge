using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TurningEdge.Debugging.DataTypes;

namespace TurningEdge.Debugging
{
    public static class Debugger
    {
        public static string OUTPUT_FILENAME = "Logs.txt";
        private static object _lock;

        static Debugger()
        {
            _lock = new object();
        }

        public static void Print(params object[] objects)
        {
            Log(LogLevel.Verbose, objects);
        }

        public static void PrintWarning(params object[] objects)
        {
            Log(LogLevel.Warning, objects);
        }

        public static void PrintError(Exception e)
        {
            Log(LogLevel.Error, e.ToString(), e.StackTrace);
        }

        private static void Log(LogLevel logLevel, params object[] objects)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string delimiter = "";

            foreach (var obj in objects)
            {
                stringBuilder.Append(delimiter);
                stringBuilder.Append(obj);
                
                delimiter = ", ";
            }

            Log(logLevel, stringBuilder.ToString());
        }

        private static void Log(LogLevel logLevel, string text)
        {
            lock (_lock)
            {
                while (true)
                {
                    try
                    {
                        string result = string.Empty;

                        result = string.Format("{0}: {1}: {2}", logLevel, DateTime.Now, text);
                        Console.WriteLine(text);
                        using (StreamWriter w = File.AppendText(OUTPUT_FILENAME))
                        {
                            w.WriteLine(result);
                        }
                        break;
                    }
                    catch (Exception)
                    { }
                }
            }
        }
    }
}
