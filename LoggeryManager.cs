using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Loggery
{
    public enum LogLevel
    {
        Trace = 5,
        Debug = 4,
        Info = 3,
        Warn = 2,
        Error = 1,
        Fatal = 0
    }

    public enum LogColor
    {
        Black = 0,
        White = 1,
        Grey = 2,
        Green = 3,
        Orange = 4,
        Red = 5,
    }

    public class LoggeryManager : MonoBehaviour
    {
        public LoggeryManager()
        {
            
        }

        public static int[] ColorChoiceIndex = new int[Enum.GetNames(typeof(LogColor)).Length];
        public static LogLevel LogLevel = LogLevel.Info;
        public static Regex RegexName = new Regex("");
        public static Regex RegexMessage = new Regex("");

        public static LoggeryLogger GetCurrentClassLogger()
        {
            var frame = new StackFrame(1, false);
            var method = frame.GetMethod();
            var declaringType = method.DeclaringType;

            var loggeryLogger = ScriptableObject.CreateInstance<LoggeryLogger>();
            if (declaringType != null) loggeryLogger.Init(declaringType.FullName, ColorChoiceIndex);
            return loggeryLogger;
        }
    }
}
