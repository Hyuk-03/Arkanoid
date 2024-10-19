using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;

enum BossState
{
    Idle,               //������ �⺻����
    Attack,             //������ ���ݻ���
}
public class BossCtrl : MonoBehaviour
{
    public int Hp;      //������ hp
    int ScoreValue;     //���ھ�

    float AttackTimer;   //������ ����Ÿ�̸�
    float AttackInterval = 2.0f;  //������ ���ݰ���

    
    Animator BossAnim;  //������ �ִϸ��̼�
    BossState m_BossState = BossState.Idle;  // ������ ����

    //�����̵�
    public float MoveSpeed = 2.0f;  // �̵� �ӵ�
    public float LeftLimit = -3.4f;  // ���� �̵� ����
    public float RightLimit = 3.4f;  // ������ �̵� ����
    private Vector3 Pos = Vector3.right;  // �ʱ� �̵� ����
    float StopTimer = 0f;  // ���� Ÿ�̸�
    bool isStop = false;  // ���� ���� ����
    //�����̵�

    //���� ����
    public GameObject m_ShootPos;           //������ġ
    public GameObject m_BossBall;           //������ ���ݺ�
    //��������

    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag("Boss"))
        {
            Hp = 10;            //������ Hp�� 10���� ����
            ScoreValue = 100;   //������ ����
        }

        BossAnim = GetComponent<Animator>();
        AttackTimer = AttackInterval; // �ʱ� Ÿ�̸� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Inst.isGameOver == true)   //���ӿ����� �ȴٸ� ������ ����,�̵��� ��������
            return;

        AttackTimer -= Time.deltaTime;          //�ð���

        if (m_BossState == BossState.Idle && AttackTimer <= 0)
        {
            Attack();                                           //�Լ�ȣ��
        }

        if (isStop==false)
        {
            Move(); // ������ �̵���Ű�� �Լ� ȣ��
        }
        else
        {
            StopTimer -= Time.deltaTime; // ���� Ÿ�̸� ����
            if (StopTimer <= 0)
            {
                isStop = false; // ���� ���� ����
            }
        }
    }

    void Move()
    {
        // ���� ��ġ�� �̵� ����� �ӵ��� ���� ���� ����
        transform.position += Pos * MoveSpeed * Time.deltaTime;

        // �̵� ���� �˻�
        if (transform.position.x >= RightLimit)
        {
            isStop = true; // ���� ���·� ��ȯ
            StopTimer = 2.0f; // 2�� ���� Ÿ�̸� ����
            Pos = Vector3.left; // ���� ���� 
        }
        else if (transform.position.x <= LeftLimit)
        {
            isStop = true; // ���� ���·� ��ȯ
            StopTimer = 2.0f; // 2�� ���� Ÿ�̸� ����
            Pos = Vector3.right; // ���� ����
        }
    }

    public void TakeDmg(int a_Value)
    {
        if (Hp <= 0)   //�����ڵ�
            return;

        Hp -= a_Value;
        if (Hp <= 0)
            Hp = 0;

        if (Hp == 0)  // hp�� 0�� ���� ��� ����
        {
            GameMgr.Inst.BossDie(ScoreValue);           //������ ���ھ� ȹ���� ���ؼ�
            Destroy(gameObject);                        //����
        }
    }
    void Attack()
    {
        m_BossState = BossState.Attack; // ���¸� Attack���� ����
        BossAnim.SetBool("attack", true); // ���� �ִϸ��̼� ����
        BossAnim.SetBool("nomal", false);  //�븻���°� �ƴϴ�
        StartCoroutine(AttackCoroutine());  //�ڷ�ƾ
    }
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1.35f); // �ִϸ��̼��� ���̿� �°� ����

        Instantiate(m_BossBall, m_ShootPos.transform.position, Quaternion.identity);

        EndAttack(); // ������ ���� �� Idle ���·� ���ư�
        AttackTimer = AttackInterval; // Ÿ�̸� ����
    }

    public void EndAttack()
    {
        BossAnim.SetBool("nomal", true);   //�븻���´�
        BossAnim.SetBool("attack", false); // ���� �ִϸ��̼� ����
        m_BossState = BossState.Idle; // ���¸� Idle�� ����
    }
}
