using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class TestContextMenuItem : MonoBehaviour
{
    //参数：
    //1 ：是否显示透明度（Alpha）
    //2：是否用HDR模式、若为true，需要下面四个参数
    [ColorUsage(false, true)]
    public Color color;


    public int num = 100;
    [ContextMenu("Execute MothodName")] // 添加一个菜单项
    public void MothodName()
    {
        Debug.Log($"num的值为：{num}");
    }


    [ContextMenuItem("Reset", "ResetBiography")]
    public string customName = "editor序列化特性";
    void ResetBiography()
    {
        customName = "reset:editor序列化特性";
    }
    public PeopleInfo peopleInfo;
    // #region 用于将代码分组，#endregion 用于标记分组的结束


    [Header("标题")]
    [Tooltip("this is a fucking tip.")]    //给监视板的字段添加小贴士,鼠标指向字段显示的提示
    [Multiline(3)]      //只有3行高度的文本框
    public string str;

    [TextArea]      //默认显示3行，超出自动显示滚动条
    public string textArea1;

    [TextArea(2, 5)]
    public string textArea2;    //最小显示2行，最大显示5行，大于五行自动显示滚动条

    [Range(1, 16)]
    public int value01 = 20;      //左右滑动条，20>16，所以初始化为超过自定义范围的值16
}
[Serializable]
public class PeopleInfo
{
    [NonSerialized]
    public int age = 20;
    public float high;
    [SerializeField]
    private float kg;
}

