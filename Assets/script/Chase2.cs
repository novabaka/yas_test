using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase2 : MonoBehaviour
{
    public string targetObjectName; // ��� �̸� string ����
    public float speed = 1; // �� �ӵ�

    float vx; // x ����

    public float AttackCoolTime;
    float AttackCoolDown;
    public static bool Attacking = false;
    bool AttackCool = false;

    bool IsGuard = false;

    int RandomAttack;

    bool range = false; // ���� ���Դ���

    GameObject targetObject; // Ÿ�� ����
    Rigidbody2D rbody; // ������ٵ� �������� �ڵ�
    Animator anim; // �ִϸ����� �������� �ڵ�

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        targetObject = GameObject.Find(targetObjectName); // Ÿ���� ã�� �ڵ�

        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        AttackCoolDown = AttackCoolTime * 50;
    }

    void Update()
    {
        if (range) // �Ÿ��ȿ� ������ �ִϸ��̼� Ȱ��ȭ
        {
            anim.SetBool("isRun", true);
        }
        if (!range) // ������ �ִϸ��̼� ��Ȱ��ȭ
        {
            anim.SetBool("isRun", false);
        }

        RandomAttack = Random.Range(1, 4);
    }

    void FixedUpdate()
    {

        if (range && !Attacking && !IsGuard) // ���� ���� �ȿ� ������ �i�ư��� ����� �ڵ� ( �������̳� ������� �ƴҶ� )
        {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation; // z���� ȸ�� ����, ��ġ ���� ����

            Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;

            vx = dir.x * speed;
            rbody.velocity = new Vector2(vx, rbody.velocity.y);

            this.GetComponent<SpriteRenderer>().flipX = (vx > 0); // �¿� �ø�
        }
        else // ���� �ȿ� �÷��̾ ���ų� �������̰ų� ������϶�
        {
            rbody.constraints = RigidbodyConstraints2D.FreezePosition; // ��ġ ����
        }

        if (Enemy_AR2.AttackRange) // �÷��̾ ���ݹ����� ��������
        {
            if (!Attacking && !AttackCool && ((RandomAttack == 1) || (RandomAttack == 3))) // �������� �ƴϸ� ��Ÿ���� �ƴϰ� ���� ���� 1�� �޾��� ��
            {
                anim.SetTrigger("isAttack"); // ���� ��� Ȱ��ȭ
                Attacking = true; // ������ ���� Ȱ��ȭ
                AttackCool = true; // ���� ��Ÿ�� ���� Ȱ��ȭ
                Invoke("Attack_ing", 0.8f); // 0.8�� �� ���� �Լ� ���� (���� ����� 0.8��)
            }
            else if (!Attacking && !AttackCool && (RandomAttack == 2)) // �������� �ƴϸ� ��Ÿ���� �ƴϰ� ���� ���� 1�� �޾��� ��
            {
                anim.SetTrigger("isGuard"); // ��� ��� Ȱ��ȭ
                IsGuard = true; // ����� ���� Ȱ��ȭ
                AttackCool = true; // ���� ��Ÿ�� ���� Ȱ��ȭ
                Invoke("Guard_ing", 1.6f); // 0.8�� �� ���� �Լ� ���� (��� ����� 1.3��)
            }
        }

        if (AttackCool) // ���� ��Ÿ�� ����
        {
            AttackCoolDown--; // ���� ��ٿ� �ʴ� 50�� ���̱�(FixedUpdate)
        }

        if (AttackCoolDown == 0) // ���� ��ٿ��� 0�� �Ǹ�
        {
            AttackCoolDown = AttackCoolTime * 50; // �������� ��Ÿ�ӿ� 50�� ���� ���� ��ٿ� ������ �Է�
            AttackCool = false; // ���� ��Ÿ�� ��Ȱ��ȭ
        }

        if (Enemy_HB2.Hit == true)
        {
            if (!IsGuard)
            {
                this.gameObject.SetActive(false);
            }
            else if (IsGuard)
            {
                targetObject.gameObject.SetActive(false);
            }
        }
    }

    private void Attack_ing() 
    {
        Attacking = false; // ������ ���� ��Ȱ��ȭ
    }

    private void Guard_ing()
    {
        IsGuard = false; // ����� ���� ��Ȱ��ȭ
    }

    void OnTriggerStay2D(UnityEngine.Collider2D collision) // Ʈ���� �����ȿ� �ݶ��̴��� ���� ���
    {
        if (collision.gameObject.tag == ("Player")) // ���� �ݶ��̴��� Player�±׸� ������ �ִٸ� range Ȱ��ȭ
        {
            range = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) // Ʈ���� ���� ������ �ݶ��̴��� ���� ���
    {
        if (collision.gameObject.tag == ("Player")) // ���� �ݶ��̴��� Player�±׸� ������ ���� �ʴٸ� range ��Ȱ��ȭ
        {
            range = false;
        }
    }
}
