using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBallCtrl : MonoBehaviour
{
    Vector3 m_DirVec;           //방향
    float m_MoveSpeed = 5.0f;  //날아가는 스피드 
    Transform Player;       //플레이어의 위치

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Paddle").transform;  //패들이라는 태그를 가진자에게

        Vector3 Pos = (Player.position - transform.position).normalized; //플레이어포지션 - 위치값
        m_DirVec = Pos; // 발사 방향 설정
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_DirVec * m_MoveSpeed * Time.deltaTime;   //날아가기
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Out"))
        {
            Destroy(gameObject);           //삭제
        }
    }
}
