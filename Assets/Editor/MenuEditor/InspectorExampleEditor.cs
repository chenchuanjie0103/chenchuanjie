using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExmapleScript))]       //自定义Inspector面板
public class InspectorExampleEditor : Editor
{
    //定义序列化属性
    private SerializedProperty objectName;
    private SerializedProperty peopleInfo;

    private void OnEnable()
    {
        //通过名字查找被序列化属性。
        objectName = serializedObject.FindProperty("objectName");
        peopleInfo = serializedObject.FindProperty("peopleInfo");
    }
    public override void OnInspectorGUI()
    {
        //表示更新序列化物体
        //通过名字查找被序列化属性。
        serializedObject.Update();
        EditorGUILayout.PropertyField(objectName);
        EditorGUILayout.PropertyField(peopleInfo);
        Debug.Log($"objectName: {objectName.stringValue}, peopleInfo: {peopleInfo}");
        //应用修改的属性值，不加的话，Inspector面板的值修改不了
        serializedObject.ApplyModifiedProperties();
    }
}

internal class ExmapleScript
{
}