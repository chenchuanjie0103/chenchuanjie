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
        selectedObject = (GameObject)EditorGUILayout.ObjectField("Ҫ�����Ķ���", selectedObject, typeof(GameObject), true);
        if (selectedObject != null)
        {
            string button = selectedObject.activeSelf ? "ʧ��" : "����";
            if (GUILayout.Button(button))
            {
                Undo.RecordObject(selectedObject, button);
                selectedObject.SetActive(!selectedObject.activeSelf);
            }

            newPosition = EditorGUILayout.Vector3Field("����", newPosition);

            if (GUILayout.Button("ȷ������"))
            {
                Undo.RecordObject(selectedObject.transform, "ȷ������");
                selectedObject.transform.position = newPosition;
            }

            if (GUILayout.Button("ɾ��"))
            {
                Undo.RecordObject(selectedObject, "ɾ��");
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