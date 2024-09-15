using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PaddleCtrl : MonoBehaviour
{
    //패들의 이동
    public float MoveSpeed = 10.0f;    //패들 이동 속도 변수
    float h = 0.0f;                    //키 입력 값 변수
    Vector3 PaddlePos = Vector3.zero;  //패들의 변수
    //패들의 이동

    //패들의 상태
    bool isBreak;   //부서진 상태
    Animator m_Anim;   //패들애니메이터
    //패들의 상태


    // Start is called before the first frame update
    void Start()
    {
        PaddlePos = transform.position; //패들의 현재 위치를 변수에 
        m_Anim = GetComponent<Animator>();  //패들 애니메이터 찾아옴
    }

    
    // Update is called once per frame
    void Update()
    {
        if (isBreak)     //패들이 부숴진상태라면 리턴
            return;

        h = Input.GetAxisRaw("Horizontal");   //유니티에 저장된 키 입력값 
        if (h != 0.0f)
        {
            if (PaddlePos != null)
            {
                PaddlePos.x += h * MoveSpeed * Time.deltaTime;          //패들의 움직이기위해서
                PaddlePos.x = Mathf.Clamp(PaddlePos.x, -3.53f, 3.53f);  //패들의 움직임 제한
                transform.position = PaddlePos;                         //위치 값 넣어주기
            }

        }
        
    }

    public void StartBreak()
    {
        StartCoroutine(Break());
    }

    IEnumerator Break()
    {
        isBreak = true;
        m_Anim.SetBool("Break", true);       //애니메이터의 break의 상태를 true로 변환 break 애니메이션 플레이
        yield return new WaitForSeconds(1.2f);  //1.2초 대기
        m_Anim.SetBool("Break", false);     //애니메이터의 break의 상태를 false로 변환 create 애니메이션 플레이
        isBreak = false;
       
    }
}
