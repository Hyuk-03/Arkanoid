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


    // Start is called before the first frame update
    void Start()
    {
        PaddlePos = transform.position; //�е��� ���� ��ġ�� ������ 
        m_Anim = GetComponent<Animator>();  //�е� �ִϸ����� ã�ƿ�
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

    public void StartBreak()
    {
        StartCoroutine(Break());
    }

    IEnumerator Break()
    {
        isBreak = true;
        m_Anim.SetBool("Break", true);       //�ִϸ������� break�� ���¸� true�� ��ȯ break �ִϸ��̼� �÷���
        yield return new WaitForSeconds(1.2f);  //1.2�� ���
        m_Anim.SetBool("Break", false);     //�ִϸ������� break�� ���¸� false�� ��ȯ create �ִϸ��̼� �÷���
        isBreak = false;
       
    }
}
