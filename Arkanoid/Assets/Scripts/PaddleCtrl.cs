using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaddleCtrl : MonoBehaviour
{
    //�е��� �̵�
    public float MoveSpeed = 10.0f;    //�е� �̵� �ӵ� ����
    float h = 0.0f;                    //Ű �Է� �� ����
    Vector3 PaddlePos = Vector3.zero;  //�е��� ����
    //�е��� �̵�

    // Start is called before the first frame update
    void Start()
    {
      PaddlePos = transform.position; //�е��� ���� ��ġ�� ������ 
    }

   



    // Update is called once per frame
    void Update()
    {
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

}
