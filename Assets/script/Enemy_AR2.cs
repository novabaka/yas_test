using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AR2 : MonoBehaviour
{
    public string targetObjectName; // ��� �̸� string ����

    public static bool AttackRange = false;

    GameObject targetObject; // Ÿ�� ����

    void Start()
    {
        targetObject = GameObject.Find(targetObjectName); // Ÿ���� ã�� �ڵ�
    }

    void OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player")) // �÷��̾ ���� �ȿ� ������ AttackRange Ȱ��ȭ
        {
            AttackRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) // ������ range ��Ȱ��ȭ
    {
        if (collision.gameObject.tag == ("Player"))
        {
            AttackRange = false;
        }
    }
}
