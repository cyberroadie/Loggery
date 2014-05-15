using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Loggery
{

    public class LoggerGui  : EditorWindow
    {
        private string[] _choices = Enum.GetValues(typeof (LogLevel)).OfType<object>().Select(o => o.ToString()).ToArray();
        private string[] _colorChoices = Enum.GetValues(typeof (LogColor)).OfType<object>().Select(o => o.ToString()).ToArray();
        private int[] _previousColorChoiceIndex = new int[Enum.GetNames(typeof(LogColor)).Length];

        private int _choiceIndex = (int) LogLevel.Info;
        private string _regexName = "";
        private string _regexMessage = "";

        SerializedProperty logLevel;

        void OnEnable()
        {

        }

        [MenuItem("Window/LoggeryLogger")]
        public static void Init()
        {
            GetWindow(typeof(LoggerGui));
        }

        public Texture2D colorPicker;
        public int ImageWidth = 100;
        public int ImageHeight = 100;

        int selected = 2;

        void OnGUI()
        {
            GUILayout.Label("LoggeryLogger Settings", EditorStyles.boldLabel);
            var previousChoiceIndex = _choiceIndex;
            _choiceIndex = EditorGUILayout.Popup("Log level ", previousChoiceIndex, _choices);

            var previousRegexName = _regexName;
            _regexName = EditorGUILayout.TextField("Regex filter class/method", _regexName);

            var previousRegexMessage = _regexMessage;
            _regexMessage = EditorGUILayout.TextField("Regex filter message", _regexMessage);

            if (_choiceIndex != previousChoiceIndex || _regexName != previousRegexName || _regexMessage != previousRegexMessage)
            {
                var loggers = GameObject.FindObjectsOfType<LoggeryLogger>();
                foreach (var LoggeryLogger in loggers)
                {
                    LoggeryLogger.logLevel = _choiceIndex;
                    LoggeryLogger.regexName = new Regex(_regexName.Trim());
                    Debug.Log("Regex name: " + _regexName.Trim());
                    LoggeryLogger.regexMessage = new Regex(_regexMessage.Trim());
                }

            }

            int i = 0;
            foreach (var choice in _choices)
            {
                LoggeryManager.ColorChoiceIndex[i] = EditorGUILayout.Popup(choice, _previousColorChoiceIndex[i], _colorChoices);
                _previousColorChoiceIndex[i] = LoggeryManager.ColorChoiceIndex[i];
                i++;
            }
        }
	
    }
}

