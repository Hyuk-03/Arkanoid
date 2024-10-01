using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    Animator m_FallItemAnim;    //애니메이터
    float FallSpeed = 2.0f;   //아이템 떨어지는 스피드

    // Start is called before the first frame update
    void Start()
    {
        m_FallItemAnim = GetComponent<Animator>();  //찾아오기
    }

    // Update is called once per frame
    void Update()
    {
        FallItem();
        transform.Translate(Vector2.down * FallSpeed * Time.deltaTime);  //아래로 내려오게 하는
    }

    void FallItem()
    {
        if (m_FallItemAnim != null)
            m_FallItemAnim.SetTrigger("fall");   //파라메터 트리거
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Out"))
        {
            Destroy(gameObject);
        }  
    }
}
