using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FallPanel : MonoBehaviour
{
    Rigidbody2D rb;  //리지드 바디

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  //리지드 바디 찾아오기
        transform.position = new Vector3(640, 1080, 0);  //판넬의 초기값
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 360)           //여기에 오면 멈춘다
        {
            rb.gravityScale = 0;                   //중력값 0
            rb.velocity = new Vector2(0.0f, 0.0f);   //벨로시티 0
        }
    }

}
