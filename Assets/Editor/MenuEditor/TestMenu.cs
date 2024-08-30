using UnityEditor;
using UnityEngine;

public class TestMenu : MonoBehaviour
{

    [MenuItem("Test+/�����˵��� %a")]
    public static void Examples()
    {
        Debug.Log("�����˵�����");
    }

    [MenuItem("Test+/Ping Selected")]
    static void Ping()
    {
        if (!Selection.activeObject)
        {
            Debug.Log("Select an object to ping");
            return;
        }

        EditorGUIUtility.PingObject(Selection.activeObject);
    }

    [MenuItem("Test+/���ȼ��˵�ѡ��/���ȼ�a", false, 2)]
    public static void Example1()
    {
        Debug.Log("�ҵ����ȼ���011");
    }
    [MenuItem("Test+/���ȼ��˵�ѡ��/���ȼ�b", false, 1)]
    public static void Example2()
    {
        Debug.Log("�ҵ����ȼ���001");
    }
    [MenuItem("Test+/���ȼ��˵�ѡ��/���ȼ�c", false, 3)]
    public static void Example3()
    {
        Debug.Log("�ҵ����ȼ���111");
    }


    [MenuItem("Test+/�����˵�ѡ��", true)]
    public static bool Example01()
    {
        return Selection.activeGameObject != null;
    }
    [MenuItem("Test+/�����˵�ѡ��", false)]
    public static void Example02()
    {
        Debug.Log(Selection.activeGameObject.name);
        //if (Selection.activeGameObject != null)
        //{
        //    Debug.Log(Selection.activeGameObject.name);
        //}
        //else
        //{
        //    Debug.Log("û��ѡ�еĶ���");
        //}
    }


    [MenuItem("Assets/Assets�²˵��� %a")]
    public static void Exmaple03()
    {
        Debug.Log("Assets�²˵���");
    }


    [MenuItem("GameObject/GameObject�²˵��� %b")]
    public static void Exmaple04()
    {
        Debug.Log("GameObject�²˵���");
    }


    [MenuItem("CONTEXT/ParticleSystem/����˵�ѡ��(������) %#a")]
    public static void Example05()
    {
        Debug.Log("��������Ĳ˵�ѡ�");
        // �����ﲻ��ʹ�� cmd����Ϊ�˷��������� MenuCommand ����
        //Debug.Log($"��ǰ������ص�GameObject��{cmd.context.name}");
    }


    [MenuItem("CONTEXT/ParticleSystem/����˵�ѡ��(����) %#b")]
    public static void Example06(MenuCommand cmd)
    {
        // MenuCommand���Ի�ȡ���ض����ϵ��������
        Debug.Log("��������Ĳ˵�ѡ�");
        Debug.Log($"��ǰ������ص�GameObject��{cmd.context.name}");
    }
}
