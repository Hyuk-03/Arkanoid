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
    public GameObject ItemPrefab;
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
                SceneManager.LoadScene("GameScene"); // �� �����
            }
        }

        if(isGameClear==true)
        {
            //����Ŭ��������϶� �����̽��� �Է�
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("BossScene");  //���� ������
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
            UpdateLifeUI();

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
        if (TotalBlocks <= 0)
        {
            ClearPanel(); // ��� ����� �ı��Ǹ� Ŭ���� �ǳ� ǥ��
        }
    }

    public void SpawnItem(Vector2 position)
    {
        
        if (Random.value < 0.2f)                    // 20% Ȯ���� ������ ����
        {
            Instantiate(ItemPrefab, position, Quaternion.identity);   //����
        }
    }

    void ClearPanel()
    {
        isGameClear = true;
        GameClearPanel.gameObject.SetActive(true);       //Ų��
        GameClearResultText.text = "<color=#FF0000>" + Score.ToString() + "</color>";  //�÷�����
        BlinkAnim m_BlinkAnim = gameObject.GetComponent<BlinkAnim>();
        if (m_BlinkAnim != null)
        {
            m_BlinkAnim.enabled = true;
        }
    }

    void GameOver()
    {
        isGameOver = true;
        GameOverPanel.gameObject.SetActive(true);         //Ų��.
        GameOverResultText.text = "<color=#FF0000>" + Score.ToString() + "</color>";  //�÷�����
        BlinkAnim m_BlinkAnim = gameObject.GetComponent<BlinkAnim>();
        if (m_BlinkAnim != null)
        {
            m_BlinkAnim.enabled = true;
        }
    }
}
