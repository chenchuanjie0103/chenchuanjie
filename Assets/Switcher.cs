using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    GameObject fireworksEjector;
    void Start()
    {
        fireworksEjector = GameObject.Find("Cylinder");
    }

    void Update()
    {
    }
    void OnMouseUp()
    {
        fireworksEjector.SetActive(!fireworksEjector.activeSelf);//�����̻�Ͱ��go�ļ���״̬
    }
}