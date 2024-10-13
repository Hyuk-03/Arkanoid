using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    public int Hp;      //������ hp
    int ScoreValue;     //���ھ�

    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag("Boss"))
        {
            Hp = 10;            //������ Hp�� 10���� ����
            ScoreValue = 100;   //������ ����
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
            GameMgr.Inst.BossDie();
            GameMgr.Inst.AddScore(ScoreValue);  //����ȹ��
            Destroy(gameObject);
        }

    }
}
