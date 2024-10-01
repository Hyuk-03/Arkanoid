using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FallPanel : MonoBehaviour
{
    Rigidbody2D rb;  //������ �ٵ�

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  //������ �ٵ� ã�ƿ���
        transform.position = new Vector3(640, 1080, 0);  //�ǳ��� �ʱⰪ
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 360)           //���⿡ ���� �����
        {
            rb.gravityScale = 0;                   //�߷°� 0
            rb.velocity = new Vector2(0.0f, 0.0f);   //���ν�Ƽ 0
        }
    }

}
