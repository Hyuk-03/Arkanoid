using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;

enum BossState
{
    Idle,               //보스의 기본상태
    Attack,             //보스의 공격상태
}
public class BossCtrl : MonoBehaviour
{
    public int Hp;      //보스의 hp
    int ScoreValue;     //스코어

    float AttackTimer;   //보스의 공격타이머
    float AttackInterval = 2.0f;  //보스의 공격간격

    
    Animator BossAnim;  //보스의 애니메이션
    BossState m_BossState = BossState.Idle;  // 보스의 상태

    //보스이동
    public float MoveSpeed = 2.0f;  // 이동 속도
    public float LeftLimit = -3.4f;  // 왼쪽 이동 제한
    public float RightLimit = 3.4f;  // 오른쪽 이동 제한
    private Vector3 Pos = Vector3.right;  // 초기 이동 방향
    float StopTimer = 0f;  // 정지 타이머
    bool isStop = false;  // 멈춤 상태 여부
    //보스이동

    //보스 공격
    public GameObject m_ShootPos;           //공격위치
    public GameObject m_BossBall;           //보스의 공격볼
    //보스공격

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
        if (GameMgr.Inst.isGameOver == true)   //게임오버가 된다면 보스의 공격,이동을 막기위해
            return;

        AttackTimer -= Time.deltaTime;          //시간값

        if (m_BossState == BossState.Idle && AttackTimer <= 0)
        {
            Attack();                                           //함수호출
        }

        if (isStop==false)
        {
            Move(); // 보스를 이동시키는 함수 호출
        }
        else
        {
            StopTimer -= Time.deltaTime; // 정지 타이머 감소
            if (StopTimer <= 0)
            {
                isStop = false; // 멈춤 상태 해제
            }
        }
    }

    void Move()
    {
        // 현재 위치에 이동 방향과 속도를 곱한 값을 더함
        transform.position += Pos * MoveSpeed * Time.deltaTime;

        // 이동 범위 검사
        if (transform.position.x >= RightLimit)
        {
            isStop = true; // 멈춤 상태로 전환
            StopTimer = 2.0f; // 2초 정지 타이머 설정
            Pos = Vector3.left; // 방향 변경 
        }
        else if (transform.position.x <= LeftLimit)
        {
            isStop = true; // 멈춤 상태로 전환
            StopTimer = 2.0f; // 2초 정지 타이머 설정
            Pos = Vector3.right; // 방향 변경
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
            GameMgr.Inst.BossDie(ScoreValue);           //보스의 스코어 획득을 위해서
            Destroy(gameObject);                        //삭제
        }
    }
    void Attack()
    {
        m_BossState = BossState.Attack; // 상태를 Attack으로 변경
        BossAnim.SetBool("attack", true); // 어택 애니메이션 시작
        BossAnim.SetBool("nomal", false);  //노말상태가 아니다
        StartCoroutine(AttackCoroutine());  //코루틴
    }
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1.35f); // 애니메이션의 길이에 맞게 조정

        Instantiate(m_BossBall, m_ShootPos.transform.position, Quaternion.identity);

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
