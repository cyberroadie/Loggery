using System;
using System.Diagnostics;
using UnityEngine;

namespace Loggery
{
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

        public static LoggeryLogger GetCurrentClassLogger()
        {
            var frame = new StackFrame(1, false);
            var method = frame.GetMethod();
            var declaringType = method.DeclaringType;

            var LoggeryLogger = ScriptableObject.CreateInstance<LoggeryLogger>();
            LoggeryLogger.Init(declaringType.FullName, ColorChoiceIndex);
            return LoggeryLogger;
        }
    }
}
