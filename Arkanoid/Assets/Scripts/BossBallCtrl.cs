using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBallCtrl : MonoBehaviour
{
    Vector3 m_DirVec;           //����
    float m_MoveSpeed = 5.0f;  //���ư��� ���ǵ� 
    Transform Player;       //�÷��̾��� ��ġ

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Paddle").transform;  //�е��̶�� �±׸� �����ڿ���

        Vector3 Pos = (Player.position - transform.position).normalized; //�÷��̾������� - ��ġ��
        m_DirVec = Pos; // �߻� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_DirVec * m_MoveSpeed * Time.deltaTime;   //���ư���
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Out"))
        {
            Destroy(gameObject);           //����
        }
    }
}
