using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Loggery
{

    public class LoggeryLogger : ScriptableObject
    {

        private string _path;
        private string _fullName;

        public LoggeryLogger()
        {
          
        }

        private string GetFullName()
        {
            var frame = new StackFrame(3, false);

            var method = frame.GetMethod();

            if (method == null)
            {
                frame = new StackFrame(2, false);
                method = frame.GetMethod();
            }

            _fullName = _path + "." + method.Name;
            return _fullName;
        }

        public string Path
        {
            get { return _path; }
        }

        private void UnityLog(LogLevel logLevel, string message)
        {
            if(LoggeryManager.RegexMessage.IsMatch(message))
                UnityEngine.Debug.Log(GetColorStartTag(logLevel) + System.DateTime.Now.ToString("HH:mm:ss.fff") + " " + logLevel + ": " + _fullName + "(): " + message + "</color>");
        }

        private string GetColorStartTag(LogLevel logLevel)
        {
            return "<color=" + ((LogColor)LoggeryManager.ColorChoiceIndex[(int)logLevel]).ToString().ToLower() + ">" + ((LogColor)LoggeryManager.ColorChoiceIndex[(int)logLevel]).ToString() + " ";
        }

        public void Trace(string message)
        {
            if (LoggeryManager.LogLevel >= LogLevel.Trace  && LoggeryManager.RegexName.IsMatch(GetFullName()))
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
            if (LoggeryManager.LogLevel >= (int)LogLevel.Fatal && LoggeryManager.RegexName.IsMatch(GetFullName()))
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
