using UnityEngine;
using UnityEditor;

public class ExampleClass7 : ScriptableObject
{
    [MenuItem("Test+/Examples/Prefs.DeleteAll Example")]
    static void deleteAllExample()
    {
        if (EditorUtility.DisplayDialog("Delete all editor preferences.",
            "Are you sure you want to delete all the editor preferences? " +
            "This action cannot be undone.", "Yes", "No"))
        {
            Debug.Log("yes");
            EditorPrefs.DeleteAll();
        }
    }
}