using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

namespace Loggery
{
    public class LoggeryLogger : ScriptableObject
    {
        private string _fullName;
        private string _path;

        public string Path
        {
            get { return _path; }
        }

        private string GetFullName()
        {
            var frame = new StackFrame(2, false);
            MethodBase method = frame.GetMethod();

            if (method.Name.Contains("MoveNext"))
            {
                string coroutineMethodName = "";
                string fullName = frame.GetMethod().ReflectedType.Name;
                if (fullName.Contains("<") && fullName.Contains(">"))
                {
                    coroutineMethodName = fullName.Substring(fullName.IndexOf('<') + 1, fullName.IndexOf('>') - 1);
                }
                _fullName = _path + "." + coroutineMethodName; 
            }
            else
            {
                _fullName = _path + "." + method.Name;    
            }
// #### START DEBUG ####
//            var stackTrace = new StackTrace();
//            for (int i = 1; i < stackTrace.FrameCount; i++)
//            {
//                frame = new StackFrame(i, false);
//
//                string coroutineMethodName;
//                string fullName = frame.GetMethod().ReflectedType.Name;
//                if (fullName.Contains("<") && fullName.Contains(">"))
//                {
//                    coroutineMethodName = fullName.Substring(fullName.IndexOf('<') + 1, fullName.IndexOf('>') - 1);
//                    UnityEngine.Debug.Log("<color=red>Coroutine method " + i + ": " + coroutineMethodName + "</color>");
//                }
//                UnityEngine.Debug.Log("<color=red>Frame method " + i + ": " + frame.GetMethod() + "</color>");                
//            }
// #### END DEBUG ####

            return _fullName;
        }

        private void UnityLog(LogLevel logLevel, string message)
        {
            if (LoggeryManager.RegexMessage.IsMatch(message))
                UnityEngine.Debug.Log(GetColorStartTag(logLevel) + DateTime.Now.ToString("HH:mm:ss.fff") + " " +
                                      logLevel + ": " + _fullName + "(): " + message + "</color>");
        }

        private string GetColorStartTag(LogLevel logLevel)
        {
            return "<color=" + ((LogColor) LoggeryManager.ColorChoiceIndex[(int) logLevel]).ToString().ToLower() + ">" +
                   ((LogColor) LoggeryManager.ColorChoiceIndex[(int) logLevel]) + " ";
        }

        public void Trace(string message)
        {
            if (LoggeryManager.LogLevel >= LogLevel.Trace && LoggeryManager.RegexName.IsMatch(GetFullName()))
            {
                UnityLog(LogLevel.Trace, message);
            }
        }

        public void Debug(string message)
        {
            if (LoggeryManager.LogLevel >= LogLevel.Debug && LoggeryManager.RegexName.IsMatch(GetFullName()))
            {
                UnityLog(LogLevel.Debug, message);
            }
        }

        public void Info(string message)
        {
            if (LoggeryManager.LogLevel >= LogLevel.Info && LoggeryManager.RegexName.IsMatch(GetFullName()))
            {
                UnityLog(LogLevel.Info, message);
            }
        }

        public void Warn(string message)
        {
            if (LoggeryManager.LogLevel >= LogLevel.Warn && LoggeryManager.RegexName.IsMatch(GetFullName()))
            {
                UnityLog(LogLevel.Warn, message);
            }
        }

        public void Error(string message)
        {
            if (LoggeryManager.LogLevel >= LogLevel.Error && LoggeryManager.RegexName.IsMatch(GetFullName()))
            {
                UnityLog(LogLevel.Error, message);
            }
        }

        public void Fatal(string message)
        {
            if (LoggeryManager.LogLevel >= (int) LogLevel.Fatal && LoggeryManager.RegexName.IsMatch(GetFullName()))
            {
                UnityLog(LogLevel.Fatal, message);
            }
        }

        public void Init(string fullName, int[] colorChoiceIndex)
        {
            _path = fullName;
        }
    }
}