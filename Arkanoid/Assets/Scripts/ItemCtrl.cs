using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    Animator m_FallItemAnim;    //�ִϸ�����
    float FallSpeed = 2.0f;   //������ �������� ���ǵ�

    // Start is called before the first frame update
    void Start()
    {
        m_FallItemAnim = GetComponent<Animator>();  //ã�ƿ���
    }

    // Update is called once per frame
    void Update()
    {
        FallItem();
        transform.Translate(Vector2.down * FallSpeed * Time.deltaTime);  //�Ʒ��� �������� �ϴ�
    }

    void FallItem()
    {
        if (m_FallItemAnim != null)
            m_FallItemAnim.SetTrigger("fall");   //�Ķ���� Ʈ����
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Out"))
        {
            Destroy(gameObject);
        }  
    }
}
