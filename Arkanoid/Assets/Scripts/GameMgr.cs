using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    [Header("���ھ�")]
    public Text ScoreText;   //���ھ��ؽ�Ʈ
    int Score;              //���ھ�
    
    //���
    int MaxLife = 2;  //�ִ� ��� ����
    int CurLife;       //������
    public Image[] Life;   //��� �̹����迭
    //���

    //������
    public GameObject ItemPrefab;       //������ ������
    //������

    [Header("���ӿ���")]
    public GameObject GameOverPanel;  //���ӿ����ǳ�
    public Text GameOverResultText;   //���ӿ������
    public bool isGameOver = false;    //���ӿ���üũ�ϱ�����
    
    [Header("����Ŭ����")]
    public GameObject GameClearPanel;  //����Ŭ�����ǳ�
    public Text GameClearResultText;    //����Ŭ������
    private int TotalBlocks;          // �� ��� ��
    public bool isGameClear = false;   //����Ŭ����üũ�ϱ� ����
    bool isBossClear = false;           //���� Ŭ���� ����

    //�̱���
    public static GameMgr Inst = null;
    //�̱���

    void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurLife = MaxLife;               //�������� �ִ����̶� ����
        UpdateLifeUI();                  //UI ������Ʈ
    }

    

    // Update is called once per frame
    void Update()
    {
        if (isGameOver==true)
        {
            // ���� ���� ������ �� �����̽��� �Է� üũ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("GameScene"); // �� �����, �ٽ��ϱ�
            }
        }

        if(isGameClear==true)
        {
            //����Ŭ��������϶� �����̽��� �Է�
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("LobbyScene");  //�κ��
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LobbyScene");  //�κ��
        }
    }

    public void AddScore(int a_Value)
    {
        Score += a_Value;                           //���ھ
        ScoreText.text = "Score" + "\n" + Score;    //���ھ �ؿ� ǥ���ϰ�;
    }

    void UpdateLifeUI()
    {
        for (int i = 0; i < Life.Length; i++)
        {
            if (i < CurLife)
                Life[i].enabled = true;  // Ȱ��ȭ
            else
                Life[i].enabled = false; // ��Ȱ��ȭ
        }
    }

    public void LoseLife()
    {
        if (CurLife > -1)
        {
            CurLife--;                //��´�
            UpdateLifeUI();           //������Ʈ
            BallCtrl ballCtrl = FindObjectOfType<BallCtrl>();
            if (ballCtrl != null)
            {
                ballCtrl.BallSpeed = 6.0f;      //������ ���� ���ǵ� �ʱ�ȭ
            }
            if (CurLife <= -1)
            {
                GameOver();         // ���� ���� ó��
            }
        }
    }
    public void SetTotalBlocks(int Count)
    {
        TotalBlocks = Count;       // �� ��� �� ����
    }

    public void BlockDestroyed()
    {
        TotalBlocks--; // ����� �ı��� ������ ����
        if (TotalBlocks <= 0 )
        {
            StartCoroutine(SpawnBoss()); // ���� ���� �Լ� ȣ��
        }
    }

    public void SpawnItem(Vector2 position)
    {
        if (Random.value < 0.1f)                    // 10% Ȯ���� ������ ����
        {
            Instantiate(ItemPrefab, position, Quaternion.identity);   //����
        }
    }

    private IEnumerator SpawnBoss()
    {
        BallCtrl a_BallCtrl = FindObjectOfType<BallCtrl>();
        if (a_BallCtrl != null)
        {
            a_BallCtrl.isBall = false;          //���� �е鿡 �������� ���� ������
            a_BallCtrl.BallTimer = 2.5f;        //���� �ð�
        }

        // ī�޶� ��鸲 ȿ�� ȣ��
        yield return StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(1.0f, 0.05f)); // ��鸲 ȣ��

        yield return new WaitForSeconds(0.5f); // ��鸲 ȿ�� ��� 

        BossGenerator a_BossGenerator = FindObjectOfType<BossGenerator>();
        if (a_BossGenerator != null)
        {
            a_BossGenerator.BossSpawn(new Vector2(0.5f, 1.7f));             //������ ������ġ
        }
    }

    void CheckGameClear()
    {
        if (TotalBlocks <= 0 && isBossClear)
        {
            ClearPanel(); // Ŭ���� �ǳ� ǥ��
        }
    }

    public void BossDie(int ScoreValue)
    {
        isBossClear = true;             //����Ŭ���� üũ ����
        AddScore(ScoreValue);           //������ ���ھ�
        CheckGameClear(); // ���� Ŭ���� üũ
    }

    void ClearPanel()
    {
        isGameClear = true;                                 //����Ŭ����üũ ���� 
        GameClearPanel.gameObject.SetActive(true);       //Ų��
        GameClearResultText.text = "<color=#FF0000>" + Score.ToString() + "</color>";  //�÷�����
        BlinkAnim m_BlinkAnim = gameObject.GetComponent<BlinkAnim>();       //�����´�
        if (m_BlinkAnim != null)
        {
            m_BlinkAnim.enabled = true;             //��ũ ��ũ��Ʈ�� �����ϰ� �Ѵ� 
        }
    }

    void GameOver()
    {
        isGameOver = true;                                //���ӿ���üũ ����
        GameOverPanel.gameObject.SetActive(true);         //Ų��.
        GameOverResultText.text = "<color=#FF0000>" + Score.ToString() + "</color>";  //�÷�����
        BlinkAnim m_BlinkAnim = gameObject.GetComponent<BlinkAnim>();       //�����´�
        if (m_BlinkAnim != null)
        {
            m_BlinkAnim.enabled = true;         //��ũ ��ũ��Ʈ �����ϰ� �Ѵ�.
        }
    }
}
