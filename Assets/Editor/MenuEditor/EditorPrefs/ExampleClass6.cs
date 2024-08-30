using UnityEngine;
using UnityEditor;

public class ExampleClass6 : EditorWindow
{
    string editorPref = "";

    [MenuItem("Test+/Examples/Prefs.DeleteKey Example")]
    static void Init()
    {
        ExampleClass6 window = GetWindowWithRect<ExampleClass6>(new Rect(0, 0, 250, 50));
        window.Show();
    }

    void OnGUI()
    {
        editorPref = EditorGUILayout.TextField("Editor key name:", editorPref);
        if (GUILayout.Button("Delete"))
            if (EditorPrefs.HasKey(editorPref))
            {
                if (EditorUtility.DisplayDialog("Removing " + editorPref + "?",
                    "Are you sure you want to " +
                    "delete the editor key " +
                    editorPref + "?, This action cant be undone",
                    "Yes",
                    "No"))
                    EditorPrefs.DeleteKey(editorPref);
            }
            else
            {
                EditorUtility.DisplayDialog("Could not find " + editorPref,
                    "Seems that " + editorPref +
                    " does not exists or it has been deleted already, " +
                    "check that you have typed correctly the name of the key.",
                    "Ok");
            }
    }
}
