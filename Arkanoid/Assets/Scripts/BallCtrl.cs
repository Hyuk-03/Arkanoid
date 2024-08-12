using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtrl : MonoBehaviour
{
    //공의 이동
    public float BallSpeed = 7.0f;       //공 이동 속도
    Vector2 DirBall;                     //공의 이동방향
    bool isBall = false;                 //공이 패들에서 떨어졌나 판단
    float BallTimer = 0.0f;              //첫 시작시 볼을 발사하기 위한
    //공의 이동

    // Start is called before the first frame update
    void Start()
    {
        DirBall = Vector2.up.normalized;  //초기 공 이동 방향 위로
        BallTimer = 1.5f;                 //시간 설정 
    }

    // Update is called once per frame
    void Update()
    {
        if(isBall == false)
        {
            Vector3 PaddlePos = GameObject.Find("Paddle").transform.position;  //패들의 위치를 찾아옴

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
        if(coll.gameObject.CompareTag("Wall")==true)
        {
            DirBall = Vector2.Reflect(DirBall, coll.contacts[0].normal);  //벽과 부딪히면 그쪽에 백터값이 생성되고 반전시킨다.
        }
        else if(coll.gameObject.CompareTag("Paddle")==true)
        {
            float HitPoint = coll.contacts[0].point.x;             //패들의 부딪히면 생성되는 백터값
            float PaddleCenter = coll.transform.position.x;         //패들 중앙 x 좌표
            float Angle = (HitPoint - PaddleCenter) * 2;            //빼고 곱한다
            DirBall = new Vector2(Mathf.Sin(Angle), Mathf.Cos(Angle)).normalized;  //공이 날아가는.

        }
        else if(coll.gameObject.CompareTag("Block")==true)
        {
            coll.gameObject.GetComponent<BlockCtrl>().TakeDmg(1);
        }
    }

   
}
