using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PaddleCtrl : MonoBehaviour
{
    //�е��� �̵�
    public float MoveSpeed = 10.0f;    //�е� �̵� �ӵ� ����
    float h = 0.0f;                    //Ű �Է� �� ����
    Vector3 PaddlePos = Vector3.zero;  //�е��� ����
    //�е��� �̵�


    //�е��� ����
    bool isBreak;   //�μ��� ����
    Animator m_Anim;   //�е�ִϸ�����
    //�е��� ����

    GameObject Ball;                //��

    // Start is called before the first frame update
    void Start()
    {
        PaddlePos = transform.position; //�е��� ���� ��ġ�� ������ 
        m_Anim = GetComponent<Animator>();  //�е� �ִϸ����� ã�ƿ�
        Ball = GameObject.Find("Ball");     //���� ã�ƿ�
    }

    
    // Update is called once per frame
    void Update()
    {
        if (isBreak)     //�е��� �ν������¶�� ����
            return;

        h = Input.GetAxisRaw("Horizontal");   //����Ƽ�� ����� Ű �Է°� 
        if (h != 0.0f)
        {
            if (PaddlePos != null)
            {
                PaddlePos.x += h * MoveSpeed * Time.deltaTime;          //�е��� �����̱����ؼ�
                PaddlePos.x = Mathf.Clamp(PaddlePos.x, -3.53f, 3.53f);  //�е��� ������ ����
                transform.position = PaddlePos;                         //��ġ �� �־��ֱ�
            }

        }
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Item"))             //������
        {
            // �������� �Ծ��� �� ���� �ӵ��� �缳��
            BallCtrl ballCtrl = FindObjectOfType<BallCtrl>();
            if (ballCtrl != null)
            {
                ballCtrl.BallSpeed = 6.0f;           //�������� ������ ���� ���ǵ� 6����
            }

            Destroy(coll.gameObject); // ������ ����
        }
        else if(coll.gameObject.CompareTag("BossBall"))     //������ ����
        {
            StartBreak();                                       //�ڷ�ƾ����
            Ball.GetComponent<BallCtrl>().StartRePlay();        //�� ��Ʈ���� �ڷ�ƾ����
            Destroy(coll.gameObject);                           //����
            GameMgr.Inst.LoseLife();                              //���
        }
    }

    public void StartBreak()
    {
        StartCoroutine(Break());                //�ڷ�ƾ����
    }

    IEnumerator Break()
    {
        isBreak = true;                     //�극��ũ ����
        m_Anim.SetBool("Break", true);       //�ִϸ������� break�� ���¸� true�� ��ȯ break �ִϸ��̼� �÷���
        yield return new WaitForSeconds(1.2f);  //1.2�� ���
        m_Anim.SetBool("Break", false);     //�ִϸ������� break�� ���¸� false�� ��ȯ create �ִϸ��̼� �÷���
        isBreak = false;                    //�극��ũ �ƴ�
    }
}
