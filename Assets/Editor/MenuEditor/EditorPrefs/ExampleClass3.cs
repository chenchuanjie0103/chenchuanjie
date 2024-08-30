using UnityEngine;
using UnityEditor;
using System;

public class ExampleClass3 : EditorWindow
{
    static float floatValue = 0.0f;

    [MenuItem("Test+/Examples/Prefs.SetFloat Example")]
    static void Init()
    {
        Rect r = new Rect(10, 10, 200, 100);
        ExampleClass3 window = (ExampleClass3)EditorWindow.GetWindowWithRect(typeof(ExampleClass3), r);
        window.Show();
    }

    void Awake()
    {
        floatValue = EditorPrefs.GetFloat("FloatExample", floatValue);
    }

    void OnGUI()
    {
        floatValue = EditorGUILayout.Slider(floatValue, -1.0f, 1.0f);
        if (GUILayout.Button("Save float " + Convert.ToString(floatValue) + "?"))
        {
            EditorPrefs.SetFloat("FloatExample", floatValue);
        }
        if (GUILayout.Button("Close"))
            this.Close();
    }
}