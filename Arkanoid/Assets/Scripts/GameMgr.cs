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
                SceneManager.LoadScene("BossScene");  //다시 씬을 재시작하여 보스로 가게끔
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
            BallCtrl ballCtrl = FindObjectOfType<BallCtrl>();
            if (ballCtrl != null)
            {
                ballCtrl.BallSpeed = 6.0f;
            }
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
            //ClearPanel(); // 모든 블록이 파괴되면 클리어 판넬 표시
            StartCoroutine(SpawnBoss()); // 보스 스폰 함수 호출
        }
    }

    public void SpawnItem(Vector2 position)
    {
        
        if (Random.value < 0.1f)                    // 10% 확률로 아이템 생성
        {
            Instantiate(ItemPrefab, position, Quaternion.identity);   //생성
        }
    }

    private IEnumerator SpawnBoss()
    {
        BallCtrl a_BallCtrl = FindObjectOfType<BallCtrl>();
        if (a_BallCtrl != null)
        {
            a_BallCtrl.isBall = false;
            a_BallCtrl.BallTimer = 2.5f;
        }

        // 카메라 흔들림 효과 호출
        yield return StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(1.0f, 0.05f)); // 흔들림 호출

        yield return new WaitForSeconds(0.5f); // 흔들림 효과 대기 

        //// 보스 생성
        //GameObject boss = Instantiate(BossPrefab, new Vector3(0, 0, 10), Quaternion.identity); // 보스 생성 위치
        //boss.GetComponent<BossController>().enabled = true; // 보스 스크립트 활성화
        //게임오브젝트를 만들고(Bossspawn) 보스제레네이션 스크립트 달고 (보스오브젝트만들기)보스프리팹으로 만들고 
        //보스 컨트롤 스크립트 만들고 보스 프리팹에 담
        //보스의 체력은 10정도이고 공격패턴은 투사체 발사. 보스가 클리어 되면 게임클리어패널나오게 하면댐...
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
