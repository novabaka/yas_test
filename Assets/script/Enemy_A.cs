using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_A : MonoBehaviour
{
    public Player PlayerScript;

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player_Hit")) // �÷��̾ ���� �ȿ� ������
        {
            if (Chase.Attackmotion == true)
            {
                PlayerScript.Player_Dead();
            }
        }
    }
}
