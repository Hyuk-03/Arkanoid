using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallCtrl : MonoBehaviour
{
    //���� �̵�
    public float BallSpeed;       //�� �̵� �ӵ�
    Vector2 DirBall;               //���� �̵�����
    public bool isBall = false;           //���� �е鿡�� �������� �Ǵ�
    public float BallTimer;              //ù ���۽� ���� �߻��ϱ� ����
    float MaxBallSpeed;         //���� �ִ�ӵ����� ����
    //���� �̵�

    GameObject Paddle;     //�е�

    // Start is called before the first frame update
    void Start()
    {
        DirBall = Vector2.up.normalized;  //�ʱ� �� �̵� ���� ����
        BallTimer = 1.5f;                 //�ð� ����
        MaxBallSpeed = 10.0f;             //�ƽ��ӵ�
        BallSpeed = 6.0f;                 //���� ó�� �ӵ�
        Paddle = GameObject.Find("Paddle");   //�е�ã�ƿ�
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Inst.isGameOver == true)  // ���� �������¶�� ���� �� �����̰� �ϱ� ���ؼ�
            return;

        if (GameMgr.Inst.isGameClear == true) //���� Ŭ������¶�� ���� �� �����̰� �ϱ� ���ؼ�
            return;

        if (isBall == false)
        {
            Vector3 PaddlePos = Paddle.transform.position;    //�е��� ��ġ

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
        if (coll.gameObject.CompareTag("Wall") == true)
        {
            DirBall = Vector2.Reflect(DirBall, coll.contacts[0].normal);  //���� �ε����� ���ʿ� ���Ͱ��� �����ǰ� ������Ų��.
        }
        else if (coll.gameObject.CompareTag("Paddle") == true)
        {
            float HitPoint = coll.contacts[0].point.x;             //�е��� x �ε����� �����Ǵ� ���Ͱ�
            float PaddleCenter = coll.transform.position.x;         //�е� �߾� x ��ǥ
            float Angle = (HitPoint - PaddleCenter) * 2;            //���� ���Ѵ�
            DirBall = new Vector2(Mathf.Sin(Angle), Mathf.Cos(Angle)).normalized;  //���� ���ư���.

        }
        else if (coll.gameObject.CompareTag("Block")==true)
        {
            BlockCtrl blockCtrl = coll.gameObject.GetComponent<BlockCtrl>();
            if (blockCtrl != null)
            {
                blockCtrl.TakeDmg(1);                         //������
                                                              //�Ի簢
                Vector2 Pos = coll.contacts[0].normal;
                Vector2 In = DirBall;
                Vector2 Out = Vector2.Reflect(In, Pos);
                DirBall = Out;                                //�ݻ簢

                if (BallSpeed < MaxBallSpeed)                //���� �ε����� �ӵ� ����
                {
                    BallSpeed += 0.125f;
                }

            }
        }

        else if (coll.gameObject.CompareTag("HardBlock")==true)
        {
            BlockCtrl blockCtrl = coll.gameObject.GetComponent<BlockCtrl>();
            if (blockCtrl != null)
            {
                blockCtrl.TakeDmg(1);
                Vector2 Pos = coll.contacts[0].normal;
                Vector2 In = DirBall;
                Vector2 Out = Vector2.Reflect(In, Pos);
                DirBall = Out;
            }
        } 
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Out")== true)
        {
            //�е鿬��
            Paddle.GetComponent<PaddleCtrl>().StartBreak();
            StartCoroutine(RePlay());
            //�е鿬��
            GameMgr.Inst.LoseLife();
        }
    }

    IEnumerator RePlay()
    {
        yield return new WaitForSeconds(3.0f);  //3�ʴ��
        isBall = false;    //���� �е鿡�� �������� �ʴ� ����
        BallTimer = 1.5f;   //�ٽ� �ð� ����
    }

}
