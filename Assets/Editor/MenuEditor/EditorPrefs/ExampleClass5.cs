using UnityEngine;
using UnityEditor;

public class ExampleClass5 : EditorWindow
{
    private string keyName = "XyZ";

    [MenuItem("Test+/Examples/Prefs.HasKey Example")]
    static void Init()
    {
        ExampleClass5 window = (ExampleClass5)EditorWindow.GetWindowWithRect(
            typeof(ExampleClass5), new Rect(0, 0, 250, 80));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Save '" + keyName + "' as Key"))
            EditorPrefs.SetString(keyName, "abc123");

        if (GUILayout.Button("Delete Key '" + keyName + "'"))
            EditorPrefs.DeleteKey(keyName);

        EditorGUILayout.EndHorizontal();

        GUILayout.Label(keyName + " key exists: " + EditorPrefs.HasKey(keyName));

        if (GUILayout.Button("Close"))
            this.Close();
    }

    // delete the key each time the demo starts
    void OnFocus()
    {
        EditorPrefs.DeleteKey(keyName);
    }
}