using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HB : MonoBehaviour
{
    public static bool Hit = false;

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player_Attack")) // �÷��̾ ���� �ȿ� ������ Hit Ȱ��ȭ
        {
            Hit = true;
        }
    }

    void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player_Attack")) // �÷��̾ ���� ������ ������ Hit ��Ȱ��ȭ
        {
            Hit = false;
        }
    }
}
