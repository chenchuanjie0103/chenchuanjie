using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class TestContextMenuItem : MonoBehaviour
{
    //������
    //1 ���Ƿ���ʾ͸���ȣ�Alpha��
    //2���Ƿ���HDRģʽ����Ϊtrue����Ҫ�����ĸ�����
    [ColorUsage(false, true)]
    public Color color;


    public int num = 100;
    [ContextMenu("Execute MothodName")] // ���һ���˵���
    public void MothodName()
    {
        Debug.Log($"num��ֵΪ��{num}");
    }


    [ContextMenuItem("Reset", "ResetBiography")]
    public string customName = "editor���л�����";
    void ResetBiography()
    {
        customName = "reset:editor���л�����";
    }
    public PeopleInfo peopleInfo;
    // #region ���ڽ�������飬#endregion ���ڱ�Ƿ���Ľ���


    [Header("����")]
    [Tooltip("this is a fucking tip.")]    //�����Ӱ���ֶ����С��ʿ,���ָ���ֶ���ʾ����ʾ
    [Multiline(3)]      //ֻ��3�и߶ȵ��ı���
    public string str;

    [TextArea]      //Ĭ����ʾ3�У������Զ���ʾ������
    public string textArea1;

    [TextArea(2, 5)]
    public string textArea2;    //��С��ʾ2�У������ʾ5�У����������Զ���ʾ������

    [Range(1, 16)]
    public int value01 = 20;      //���һ�������20>16�����Գ�ʼ��Ϊ�����Զ��巶Χ��ֵ16
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

