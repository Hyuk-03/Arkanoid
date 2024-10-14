using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BossState
{
    Idle,
    Attack,
    Die
}
public class BossCtrl : MonoBehaviour
{
    public int Hp;      //������ hp
    int ScoreValue;     //���ھ�

    float AttackTimer;   //������ ����Ÿ�̸�
    float AttackInterval = 2.0f;  //������ ���ݰ���

    Animator BossAnim;  //������ �ִϸ��̼�
    BossState m_BossState = BossState.Idle;  // ������ ����

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
        AttackTimer -= Time.deltaTime;

        if (m_BossState == BossState.Idle && AttackTimer <= 0)
        {
            Attack();
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
            GameMgr.Inst.BossDie();
            GameMgr.Inst.AddScore(ScoreValue);  //����ȹ��
            Destroy(gameObject);
        }
    }
    void Attack()
    {
        m_BossState = BossState.Attack; // ���¸� Attack���� ����
        BossAnim.SetBool("attack", true); // ���� �ִϸ��̼� ����
        BossAnim.SetBool("nomal", false);  //�븻���°� �ƴϴ�
        StartCoroutine(AttackCoroutine());
    
    }
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1.0f); // �ִϸ��̼��� ���̿� �°� ����

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
