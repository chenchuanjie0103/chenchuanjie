using UnityEngine;
using UnityEditor;

public class ExampleClass2 : EditorWindow
{
    int intValue = 42;

    [MenuItem("Test+/Examples/Prefs.SetInt Example")]
    static void Init()
    {
        ExampleClass2 window = (ExampleClass2)EditorWindow.GetWindow(typeof(ExampleClass2));
        window.Show();
    }

    void OnGUI()
    {
        int temp;
        temp = EditorPrefs.GetInt("SetIntExample", -1);
        EditorGUILayout.LabelField("Current stored value: " + temp.ToString());
        intValue = EditorGUILayout.IntField("Value to write to Prefs: ", intValue);
        if (GUILayout.Button("Save value: " + intValue.ToString()))
        {
            EditorPrefs.SetInt("SetIntExample", intValue);
            Debug.Log("SetInt: " + intValue);
        }
    }
}