using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Loggery
{
    public class LoggerGui : EditorWindow
    {
        private readonly string[] _choices =
            Enum.GetValues(typeof (LogLevel)).OfType<object>().Select(o => o.ToString()).ToArray();

        private readonly string[] _colorChoices =
            Enum.GetValues(typeof (LogColor)).OfType<object>().Select(o => o.ToString()).ToArray();

        private readonly int[] _previousColorChoiceIndex = new int[Enum.GetNames(typeof (LogColor)).Length];
        public int ImageHeight = 100;
        public int ImageWidth = 100;

        private int _choiceIndex = (int) LogLevel.Info;
        private string _regexMessage = "";
        private string _regexName = "";
        public Texture2D colorPicker;

        private SerializedProperty logLevel;
        private int selected = 2;

        private void OnEnable()
        {
        }

        [MenuItem("Window/LoggeryLogger")]
        public static void Init()
        {
            GetWindow(typeof (LoggerGui));
        }

        private void OnGUI()
        {
            GUILayout.Label("LoggeryLogger Settings", EditorStyles.boldLabel);
            int previousChoiceIndex = _choiceIndex;
            _choiceIndex = EditorGUILayout.Popup("Log level ", previousChoiceIndex, _choices);

            string previousRegexName = _regexName;
            _regexName = EditorGUILayout.TextField("Regex filter class/method", _regexName);

            string previousRegexMessage = _regexMessage;
            _regexMessage = EditorGUILayout.TextField("Regex filter message", _regexMessage);

            if (_choiceIndex != previousChoiceIndex || _regexName != previousRegexName ||
                _regexMessage != previousRegexMessage)
            {
                LoggeryManager.LogLevel = (LogLevel) _choiceIndex;
                LoggeryManager.RegexName = new Regex(_regexName.Trim());
                LoggeryManager.RegexMessage = new Regex(_regexMessage.Trim());
            }

            int i = 0;
            foreach (string choice in _choices)
            {
                LoggeryManager.ColorChoiceIndex[i] = EditorGUILayout.Popup(choice, _previousColorChoiceIndex[i],
                    _colorChoices);
                _previousColorChoiceIndex[i] = LoggeryManager.ColorChoiceIndex[i];
                i++;
            }
        }
    }
}