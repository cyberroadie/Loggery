using System.Diagnostics;
using System.Runtime.InteropServices;
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

    public class LoggeryLogger : ScriptableObject
    {

        public int logLevel = (int) LogLevel.Info;

        public Regex regexMessage = new Regex("");
        public Regex regexName = new Regex("");

        private string _path;
        private string _fullName;

        public LoggeryLogger()
        {
          
        }

        private string GetFullName()
        {
            var frame = new StackFrame(3, false);
            var method = frame.GetMethod();
            _fullName = _path + "." + method.Name;
            return _fullName;
        }

        public string Path
        {
            get { return _path; }
        }

        private void UnityLog(LogLevel logLevel, string message)
        {
            if(regexMessage.IsMatch(message))
                UnityEngine.Debug.Log(GetColorStartTag(logLevel) + System.DateTime.Now.ToString("HH:mm:ss.fff") + " " + logLevel + ": " + _fullName + "(): " + message + "</color>");
        }

        private string GetColorStartTag(LogLevel logLevel)
        {
            return "<color=" + ((LogColor)LoggeryManager.ColorChoiceIndex[(int)logLevel]).ToString().ToLower() + ">" + ((LogColor)LoggeryManager.ColorChoiceIndex[(int)logLevel]).ToString() + " ";
        }

        public void Trace(string message)
        {
            if (logLevel >= (int) LogLevel.Trace  && regexName.IsMatch(GetFullName()))
            {
                UnityLog(LogLevel.Trace, message);
            }           
        }

        public void Debug(string message)
        {
            if (logLevel >= (int)LogLevel.Debug && regexName.IsMatch(GetFullName()))
            {
                UnityLog(LogLevel.Debug, message);
            }
        }

        public void Info(string message)
        {
            if (logLevel >= (int)LogLevel.Info && regexName.IsMatch(GetFullName()))
            {
                UnityLog(LogLevel.Info, message);
            }
        }

        public void Warn(string message)
        {
            if (logLevel >= (int)LogLevel.Warn && regexName.IsMatch(GetFullName()))
            {
                UnityLog(LogLevel.Warn, message);
            }
        }

        public void Error(string message)
        {
            if (logLevel >= (int)LogLevel.Error && regexName.IsMatch(GetFullName()))
            {
                UnityLog(LogLevel.Error, message);
            }
        }

        public void Fatal(string message)
        {
            if (logLevel >= (int)LogLevel.Fatal && regexName.IsMatch(GetFullName()))
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
