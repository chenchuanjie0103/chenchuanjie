using UnityEngine;
using UnityEditor;
//EditorPrefs 不支持 null 字符串，而是存储空字符串
public class ExampleClass1 : EditorWindow
{
    string note = "Notes:\n->\n->";
    [MenuItem("Test+/Examples/Prefs.SetString Example")]
    static void Init()
    {
        ExampleClass1 window = (ExampleClass1)EditorWindow.GetWindow(typeof(ExampleClass1));
        window.Show();
    }
    void OnGUI()
    {
        note = EditorGUILayout.TextArea(note,
            GUILayout.Width(position.width - 5),
            GUILayout.Height(position.height - 30));
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset"))
            note = "重置";
        if (GUILayout.Button("Clear Story", GUILayout.Width(72)))
        {
            note = "Notes:\n->\n->";
        }
        EditorGUILayout.EndHorizontal();
    }
    void OnFocus()
    {
        if (EditorPrefs.HasKey("SetStringExample"))
            note = EditorPrefs.GetString("SetStringExample");
    }
    void OnLostFocus()
    {
        EditorPrefs.SetString("SetStringExample", note);
    }
    void OnDestroy()
    {
        EditorPrefs.SetString("Prefs.SetString Example", note);
    }
}