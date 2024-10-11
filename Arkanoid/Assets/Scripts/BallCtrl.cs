using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallCtrl : MonoBehaviour
{
    //공의 이동
    public float BallSpeed;       //공 이동 속도
    Vector2 DirBall;               //공의 이동방향
    public bool isBall = false;           //공이 패들에서 떨어졌나 판단
    public float BallTimer;              //첫 시작시 볼을 발사하기 위한
    float MaxBallSpeed;         //공의 최대속도설정 변수
    //공의 이동

    GameObject Paddle;     //패들

    // Start is called before the first frame update
    void Start()
    {
        DirBall = Vector2.up.normalized;  //초기 공 이동 방향 위로
        BallTimer = 1.5f;                 //시간 설정
        MaxBallSpeed = 10.0f;             //맥스속도
        BallSpeed = 6.0f;                 //공의 처음 속도
        Paddle = GameObject.Find("Paddle");   //패들찾아옴
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Inst.isGameOver == true)  // 게임 오버상태라면 공을 못 움직이게 하기 위해서
            return;

        if (GameMgr.Inst.isGameClear == true) //게임 클리어상태라면 공을 못 움직이게 하기 위해서
            return;

        if (isBall == false)
        {
            Vector3 PaddlePos = Paddle.transform.position;    //패들의 위치

            Vector3 BallPos = PaddlePos;  //공의 위치를 패들위치에
            BallPos.y = -3.22f;            //패들과 공 사이의 간격
            transform.position = BallPos;  //패들 위에 공을 고정


            BallTimer -= Time.deltaTime;     //시간이 지나면
            if (BallTimer <= 0.0f)    
            {
                isBall = true;
                DirBall = new Vector2(Random.Range(-1f, 1f), 1).normalized;   //랜덤발사
                
            }
            
        }
        else
        {
            transform.Translate(DirBall * BallSpeed * Time.deltaTime);  //공 이동
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Wall") == true)
        {
            DirBall = Vector2.Reflect(DirBall, coll.contacts[0].normal);  //벽과 부딪히면 그쪽에 백터값이 생성되고 반전시킨다.
        }
        else if (coll.gameObject.CompareTag("Paddle") == true)
        {
            float HitPoint = coll.contacts[0].point.x;             //패들의 x 부딪히면 생성되는 백터값
            float PaddleCenter = coll.transform.position.x;         //패들 중앙 x 좌표
            float Angle = (HitPoint - PaddleCenter) * 2;            //빼고 곱한다
            DirBall = new Vector2(Mathf.Sin(Angle), Mathf.Cos(Angle)).normalized;  //공이 날아가는.

        }
        else if (coll.gameObject.CompareTag("Block")==true)
        {
            BlockCtrl blockCtrl = coll.gameObject.GetComponent<BlockCtrl>();
            if (blockCtrl != null)
            {
                blockCtrl.TakeDmg(1);                         //데미지
                                                              //입사각
                Vector2 Pos = coll.contacts[0].normal;
                Vector2 In = DirBall;
                Vector2 Out = Vector2.Reflect(In, Pos);
                DirBall = Out;                                //반사각

                if (BallSpeed < MaxBallSpeed)                //블럭과 부딪히면 속도 증가
                {
                    BallSpeed += 0.125f;
                }

            }
        }

        else if (coll.gameObject.CompareTag("HardBlock")==true)
        {
            BlockCtrl blockCtrl = coll.gameObject.GetComponent<BlockCtrl>();
            if (blockCtrl != null)
            {
                blockCtrl.TakeDmg(1);
                Vector2 Pos = coll.contacts[0].normal;
                Vector2 In = DirBall;
                Vector2 Out = Vector2.Reflect(In, Pos);
                DirBall = Out;
            }
        } 
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Out")== true)
        {
            //패들연출
            Paddle.GetComponent<PaddleCtrl>().StartBreak();
            StartCoroutine(RePlay());
            //패들연출
            GameMgr.Inst.LoseLife();
        }
    }

    IEnumerator RePlay()
    {
        yield return new WaitForSeconds(3.0f);  //3초대기
        isBall = false;    //공이 패들에서 떨어지지 않는 상태
        BallTimer = 1.5f;   //다시 시간 충전
    }

}
