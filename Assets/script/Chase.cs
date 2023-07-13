using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public string targetObjectName; // ��� �̸� string ����
    public float speed = 1; // �� �ӵ�

    float vx; // x ����

    public float AttackCoolTime;
    float AttackCoolDown;
    bool Attacking = false;
    public static bool Attackmotion = false;
    bool AttackCool = false;

    public static bool isLeft = true;

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
        if (range)
        {
            anim.SetBool("isRun", true);
        }
        if (!range)
        {
            anim.SetBool("isRun", false);
        }
    }

    void FixedUpdate()
    {

        if (range && !Attacking) // ���� ���� �ȿ� ������ �i�ư��� ����� �ڵ� ( 
        {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            Vector3 dir = (targetObject.transform.position - this.transform.position).normalized; 

            vx = dir.x * speed;
            rbody.velocity = new Vector2(vx, rbody.velocity.y);

            this.GetComponent<SpriteRenderer>().flipX = (vx > 0); // �¿� �ø�

            if (vx > 0)
            {
                if (isLeft)
                {
                    isLeft = false;
                }
                else if (!isLeft)
                {
                    isLeft = true;
                }
            }
        }
        else
        {
            rbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        if (Enemy_AR.AttackRange)
        {
            if (!Attacking && !AttackCool)
            {
                anim.SetTrigger("isAttack");
                Attacking = true;
                AttackCool = true;
                Invoke("Attack_ing1", 0.15f);
            }
        }

        if (AttackCool)
        {
            AttackCoolDown--;
        }

        if (AttackCoolDown == 0)
        {
            AttackCoolDown = AttackCoolTime * 50;
            AttackCool = false;
        }

        if (Enemy_HB.Hit == true)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Attack_ing1()
    {
        Attackmotion = true;
        Invoke("Attack_ing2", 0.45f);
        Invoke("Attack_ing3", 0.3f);
    }

    private void Attack_ing2()
    {
        Attacking = false;
    }

    private void Attack_ing3()
    {
        Attackmotion = false;
    }


    void OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player")) // �÷��̾ ���� �ȿ� ������ range Ȱ��ȭ
        {
            range = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) // ������ range ��Ȱ��ȭ
    {
        if (collision.gameObject.tag == ("Player"))
        {
            range = false;
        }
    }
}
