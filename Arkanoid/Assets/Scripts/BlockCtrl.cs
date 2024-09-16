using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCtrl : MonoBehaviour
{
    public int Hp;  //���� hp
    int ScoreValue;   //���ھ� 

    void Start()
    {
        if (CompareTag("HardBlock"))
        {
            Hp = 2;  // �ϵ���� Hp�� 2�� ����
            ScoreValue = 20;    //�ϵ������
        }
        else
        {
            Hp = 1;  // �⺻ ���� Hp�� 1�� ����
            ScoreValue = 10;   //�⺻�� ����
        }
    }

    public void TakeDmg(int a_Value)
    {
        if (Hp <= 0)   //�����ڵ�
            return;

        Hp -= a_Value;
        if (Hp <= 0)
            Hp = 0;

        if (Hp == 0)  // hp�� 0�� ���� ��� ����
        {
            StartCoroutine(Break());
            GameMgr.Inst.AddScore(ScoreValue);  //����ȹ��
        }

    }

    IEnumerator Break()
    {
        yield return new WaitForSeconds(0.1f);  
        Destroy(gameObject);   //������
    }

}
