using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaddleCtrl : MonoBehaviour
{
    //패들의 이동
    public float MoveSpeed = 10.0f;    //패들 이동 속도 변수
    float h = 0.0f;                    //키 입력 값 변수
    Vector3 PaddlePos = Vector3.zero;  //패들의 변수
    //패들의 이동

    // Start is called before the first frame update
    void Start()
    {
      PaddlePos = transform.position; //패들의 현재 위치를 변수에 
    }

   



    // Update is called once per frame
    void Update()
    {
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

}
