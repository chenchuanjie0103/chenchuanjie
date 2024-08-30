using UnityEditor;
using UnityEngine;

public class TestMenu : MonoBehaviour
{

    [MenuItem("Test+/顶部菜单栏 %a")]
    public static void Examples()
    {
        Debug.Log("顶部菜单栏！");
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

    [MenuItem("Test+/优先级菜单选项/优先级a", false, 2)]
    public static void Example1()
    {
        Debug.Log("我的优先级是011");
    }
    [MenuItem("Test+/优先级菜单选项/优先级b", false, 1)]
    public static void Example2()
    {
        Debug.Log("我的优先级是001");
    }
    [MenuItem("Test+/优先级菜单选项/优先级c", false, 3)]
    public static void Example3()
    {
        Debug.Log("我的优先级是111");
    }


    [MenuItem("Test+/条件菜单选项", true)]
    public static bool Example01()
    {
        return Selection.activeGameObject != null;
    }
    [MenuItem("Test+/条件菜单选项", false)]
    public static void Example02()
    {
        Debug.Log(Selection.activeGameObject.name);
        //if (Selection.activeGameObject != null)
        //{
        //    Debug.Log(Selection.activeGameObject.name);
        //}
        //else
        //{
        //    Debug.Log("没有选中的对象");
        //}
    }


    [MenuItem("Assets/Assets下菜单栏 %a")]
    public static void Exmaple03()
    {
        Debug.Log("Assets下菜单栏");
    }


    [MenuItem("GameObject/GameObject下菜单栏 %b")]
    public static void Exmaple04()
    {
        Debug.Log("GameObject下菜单栏");
    }


    [MenuItem("CONTEXT/ParticleSystem/组件菜单选项(不带参) %#a")]
    public static void Example05()
    {
        Debug.Log("这是组件的菜单选项！");
        // 在这里不能使用 cmd，因为此方法不接受 MenuCommand 参数
        //Debug.Log($"当前组件挂载的GameObject是{cmd.context.name}");
    }


    [MenuItem("CONTEXT/ParticleSystem/组件菜单选项(带参) %#b")]
    public static void Example06(MenuCommand cmd)
    {
        // MenuCommand可以获取挂载对象上的相关属性
        Debug.Log("这是组件的菜单选项！");
        Debug.Log($"当前组件挂载的GameObject是{cmd.context.name}");
    }
}
