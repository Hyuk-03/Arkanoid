using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameMgr : MonoBehaviour
{
    //스코어 텍스트
    public Text ScoreText;   //스코어텍스트
    int Score;              //스코어
    //스코어 텍스트

    //목숨
    int MaxLife = 2;  //최대 목숨 개수
    int CurLife;       //현재목숨
    public Image[] Life;   //목숨 이미지배열
    //목숨

    //게임오버
    public GameObject GameOverPanel;
    public Text ResultText;
    //게임오버

    //싱글톤
    public static GameMgr Inst = null;
    //싱글톤

    void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurLife = MaxLife;               //현재목숨을 최대목숨이랑 같게
        UpdateLifeUI();                  //UI 업데이트
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int a_Value)
    {
        Score += a_Value;
        ScoreText.text = "Score" + "\n" + Score;    //스코어를 밑에 표기하고싶어서
    }

    void UpdateLifeUI()
    {
        for (int i = 0; i < Life.Length; i++)
        {
            if (i < CurLife)
                Life[i].enabled = true;  // 활성화
            else
                Life[i].enabled = false; // 비활성화
        }
    }

    public void LoseLife()
    {
        if (CurLife > -1)
        {
            CurLife--;                //깍는다
            UpdateLifeUI();

            if (CurLife <= -1)
            {
                GameOver();         // 게임 오버 처리
            }
        }

    }

    void GameOver()
    {
        Time.timeScale = 0;            //일시정지
        GameOverPanel.gameObject.SetActive(true);         //킨다.
        ResultText.text = "<color=#FF0000>" + Score.ToString() + "</color>";  //컬러변경
    }
}
