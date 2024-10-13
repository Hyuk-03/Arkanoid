using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    public int Hp;      //보스의 hp
    int ScoreValue;     //스코어

    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag("Boss"))
        {
            Hp = 10;            //보스의 Hp는 10으로 설정
            ScoreValue = 100;   //보스의 점수
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(int a_Value)
    {
        if (Hp <= 0)   //방지코드
            return;

        Hp -= a_Value;
        if (Hp <= 0)
            Hp = 0;

        if (Hp == 0)  // hp가 0일 때만 블록 삭제
        {
            GameMgr.Inst.BossDie();
            GameMgr.Inst.AddScore(ScoreValue);  //점수획득
            Destroy(gameObject);
        }

    }
}
