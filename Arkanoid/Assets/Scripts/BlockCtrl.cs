using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCtrl : MonoBehaviour
{
    public int Hp;  //블럭의 hp
    int ScoreValue;   //스코어 

    void Start()
    {
        if (CompareTag("HardBlock"))
        {
            Hp = 2;  // 하드블럭의 Hp는 2로 설정
            ScoreValue = 20;    //하드블럭점수
        }
        else
        {
            Hp = 1;  // 기본 블럭의 Hp는 1로 설정
            ScoreValue = 10;   //기본블럭 점수
        }
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
            StartCoroutine(Break());
            GameMgr.Inst.AddScore(ScoreValue);  //점수획득
        }

    }

    IEnumerator Break()
    {
        yield return new WaitForSeconds(0.1f);  
        Destroy(gameObject);   //블럭삭제
    }

}
