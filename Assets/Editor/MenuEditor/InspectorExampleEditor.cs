using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExmapleScript))]       //�Զ���Inspector���
public class InspectorExampleEditor : Editor
{
    //�������л�����
    private SerializedProperty objectName;
    private SerializedProperty peopleInfo;

    private void OnEnable()
    {
        //ͨ�����ֲ��ұ����л����ԡ�
        objectName = serializedObject.FindProperty("objectName");
        peopleInfo = serializedObject.FindProperty("peopleInfo");
    }
    public override void OnInspectorGUI()
    {
        //��ʾ�������л�����
        //ͨ�����ֲ��ұ����л����ԡ�
        serializedObject.Update();
        EditorGUILayout.PropertyField(objectName);
        EditorGUILayout.PropertyField(peopleInfo);
        Debug.Log($"objectName: {objectName.stringValue}, peopleInfo: {peopleInfo}");
        //Ӧ���޸ĵ�����ֵ�����ӵĻ���Inspector����ֵ�޸Ĳ���
        serializedObject.ApplyModifiedProperties();
    }
}

internal class ExmapleScript
{
}