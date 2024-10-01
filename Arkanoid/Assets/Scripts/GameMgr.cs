using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameMgr : MonoBehaviour
{
    [Header("스코어")]
    public Text ScoreText;   //스코어텍스트
    int Score;              //스코어
    
    //목숨
    int MaxLife = 2;  //최대 목숨 개수
    int CurLife;       //현재목숨
    public Image[] Life;   //목숨 이미지배열
    //목숨

    //아이템
    public GameObject ItemPrefab;
    //아이템

    [Header("게임오버")]
    public GameObject GameOverPanel;  //게임오버판넬
    public Text GameOverResultText;   //게임오버결과
    public bool isGameOver = false;    //게임오버체크하기위한
    
    [Header("게임클리어")]
    public GameObject GameClearPanel;  //게임클리어판넬
    public Text GameClearResultText;    //게임클리어결과
    private int TotalBlocks;          // 총 블록 수
    public bool isGameClear = false;   //게임클리어체크하기 위한


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
        if (isGameOver==true)
        {
            // 게임 오버 상태일 때 스페이스바 입력 체크
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("GameScene"); // 씬 재시작
            }
        }

        if(isGameClear==true)
        {
            //게임클리어상태일때 스페이스파 입력
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("BossScene");  //보스 씬으로
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LobbyScene");
        }
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
    public void SetTotalBlocks(int Count)
    {
        TotalBlocks = Count;       // 총 블록 수 설정
    }

    public void BlockDestroyed()
    {
        TotalBlocks--; // 블록이 파괴될 때마다 감소
        if (TotalBlocks <= 0)
        {
            ClearPanel(); // 모든 블록이 파괴되면 클리어 판넬 표시
        }
    }

    public void SpawnItem(Vector2 position)
    {
        
        if (Random.value < 0.2f)                    // 20% 확률로 아이템 생성
        {
            Instantiate(ItemPrefab, position, Quaternion.identity);   //생성
        }
    }

    void ClearPanel()
    {
        isGameClear = true;
        GameClearPanel.gameObject.SetActive(true);       //킨다
        GameClearResultText.text = "<color=#FF0000>" + Score.ToString() + "</color>";  //컬러변경
        BlinkAnim m_BlinkAnim = gameObject.GetComponent<BlinkAnim>();
        if (m_BlinkAnim != null)
        {
            m_BlinkAnim.enabled = true;
        }
    }

    void GameOver()
    {
        isGameOver = true;
        GameOverPanel.gameObject.SetActive(true);         //킨다.
        GameOverResultText.text = "<color=#FF0000>" + Score.ToString() + "</color>";  //컬러변경
        BlinkAnim m_BlinkAnim = gameObject.GetComponent<BlinkAnim>();
        if (m_BlinkAnim != null)
        {
            m_BlinkAnim.enabled = true;
        }
    }
}
