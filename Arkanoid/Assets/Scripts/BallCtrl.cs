using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtrl : MonoBehaviour
{
    //���� �̵�
    public float BallSpeed = 7.0f;       //�� �̵� �ӵ�
    Vector2 DirBall;                     //���� �̵�����
    bool isBall = false;                 //���� �е鿡�� �������� �Ǵ�
    float BallTimer = 0.0f;              //ù ���۽� ���� �߻��ϱ� ����
    //���� �̵�

    // Start is called before the first frame update
    void Start()
    {
        DirBall = Vector2.up.normalized;  //�ʱ� �� �̵� ���� ����
        BallTimer = 1.5f;                 //�ð� ���� 
    }

    // Update is called once per frame
    void Update()
    {
        if(isBall == false)
        {
            Vector3 PaddlePos = GameObject.Find("Paddle").transform.position;  //�е��� ��ġ�� ã�ƿ�

            Vector3 BallPos = PaddlePos;  //���� ��ġ�� �е���ġ��
            BallPos.y = -3.22f;            //�е�� �� ������ ����
            transform.position = BallPos;  //�е� ���� ���� ����


            BallTimer -= Time.deltaTime;     //�ð��� ������
            if (BallTimer <= 0.0f)    
            {
                isBall = true;
                DirBall = new Vector2(Random.Range(-1f, 1f), 1).normalized;   //�����߻�
            }
            
        }
        else
        {
            transform.Translate(DirBall * BallSpeed * Time.deltaTime);  //�� �̵�
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("Wall")==true)
        {
            DirBall = Vector2.Reflect(DirBall, coll.contacts[0].normal);  //���� �ε����� ���ʿ� ���Ͱ��� �����ǰ� ������Ų��.
        }
        else if(coll.gameObject.CompareTag("Paddle")==true)
        {
            float HitPoint = coll.contacts[0].point.x;             //�е��� �ε����� �����Ǵ� ���Ͱ�
            float PaddleCenter = coll.transform.position.x;         //�е� �߾� x ��ǥ
            float Angle = (HitPoint - PaddleCenter) * 2;            //���� ���Ѵ�
            DirBall = new Vector2(Mathf.Sin(Angle), Mathf.Cos(Angle)).normalized;  //���� ���ư���.

        }
        else if(coll.gameObject.CompareTag("Block")==true)
        {
            coll.gameObject.GetComponent<BlockCtrl>().TakeDmg(1);
        }
    }

   
}
