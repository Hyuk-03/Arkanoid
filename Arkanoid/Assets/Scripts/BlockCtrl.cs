using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCtrl : MonoBehaviour
{
    public int Hp;  //���� hp

    void Start()
    {
        if (CompareTag("HardBlock"))
        {
            Hp = 2;  // �ϵ���� Hp�� 2�� ����
        }
        else
        {
            Hp = 1;  // �⺻ ���� Hp�� 1�� ����
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
        }

    }

    IEnumerator Break()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}
