using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CCJ : EditorWindow
{
    private GameObject selectedObject;
    private Vector3 newPosition = Vector3.zero;

    [MenuItem("Test+/Homework")]
    public static void ShowWindow()
    {
        GetWindow<CCJ>("Homework");
    }

    private void OnGUI()
    {
        selectedObject = (GameObject)EditorGUILayout.ObjectField("要操作的对象", selectedObject, typeof(GameObject), true);
        if (selectedObject != null)
        {
            string button = selectedObject.activeSelf ? "失活" : "激活";
            if (GUILayout.Button(button))
            {
                Undo.RecordObject(selectedObject, button);
                selectedObject.SetActive(!selectedObject.activeSelf);
            }

            newPosition = EditorGUILayout.Vector3Field("坐标", newPosition);

            if (GUILayout.Button("确定坐标"))
            {
                Undo.RecordObject(selectedObject.transform, "确定坐标");
                selectedObject.transform.position = newPosition;
            }

            if (GUILayout.Button("删除"))
            {
                Undo.RecordObject(selectedObject, "删除");
                Remove(selectedObject);
            }
        }
    }

    private void Remove(GameObject gameObject)
    {
        var components = gameObject.GetComponents<Component>();
        foreach (var component in components)
        {
            if (!(component is Transform))
            {
                DestroyImmediate(component);
            }
        }
    }
}