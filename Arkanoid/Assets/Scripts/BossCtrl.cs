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
    public int Hp;      //보스의 hp
    int ScoreValue;     //스코어

    float AttackTimer;   //보스의 공격타이머
    float AttackInterval = 2.0f;  //보스의 공격간격

    Animator BossAnim;  //보스의 애니메이션
    BossState m_BossState = BossState.Idle;  // 보스의 상태

    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag("Boss"))
        {
            Hp = 10;            //보스의 Hp는 10으로 설정
            ScoreValue = 100;   //보스의 점수
        }

        BossAnim = GetComponent<Animator>();
        AttackTimer = AttackInterval; // 초기 타이머 설정
       
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
        if (Hp <= 0)   //방지코드
            return;

        Hp -= a_Value;
        if (Hp <= 0)
            Hp = 0;

        if (Hp == 0)  // hp가 0일 때만 블록 삭제
        {
            GameMgr.Inst.BossDie();
            GameMgr.Inst.AddScore(ScoreValue);  //점수획득
            Destroy(gameObject);
        }
    }
    void Attack()
    {
        m_BossState = BossState.Attack; // 상태를 Attack으로 변경
        BossAnim.SetBool("attack", true); // 어택 애니메이션 시작
        BossAnim.SetBool("nomal", false);  //노말상태가 아니다
        StartCoroutine(AttackCoroutine());
    
    }
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1.0f); // 애니메이션의 길이에 맞게 조정

        EndAttack(); // 공격이 끝난 후 Idle 상태로 돌아감
        AttackTimer = AttackInterval; // 타이머 리셋
    }

    public void EndAttack()
    {
        BossAnim.SetBool("nomal", true);   //노말상태다
        BossAnim.SetBool("attack", false); // 어택 애니메이션 종료
        m_BossState = BossState.Idle; // 상태를 Idle로 변경
    }
}
