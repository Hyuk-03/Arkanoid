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
                SceneManager.LoadScene("BossScene");  //�ٽ� ���� ������Ͽ� ������ ���Բ�
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
            BallCtrl ballCtrl = FindObjectOfType<BallCtrl>();
            if (ballCtrl != null)
            {
                ballCtrl.BallSpeed = 6.0f;
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
        if (TotalBlocks <= 0)
        {
            //ClearPanel(); // ��� ����� �ı��Ǹ� Ŭ���� �ǳ� ǥ��
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
            a_BallCtrl.isBall = false;
            a_BallCtrl.BallTimer = 2.5f;
        }

        // ī�޶� ��鸲 ȿ�� ȣ��
        yield return StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(1.0f, 0.05f)); // ��鸲 ȣ��

        yield return new WaitForSeconds(0.5f); // ��鸲 ȿ�� ��� 

        //// ���� ����
        //GameObject boss = Instantiate(BossPrefab, new Vector3(0, 0, 10), Quaternion.identity); // ���� ���� ��ġ
        //boss.GetComponent<BossController>().enabled = true; // ���� ��ũ��Ʈ Ȱ��ȭ
        //���ӿ�����Ʈ�� �����(Bossspawn) �����������̼� ��ũ��Ʈ �ް� (����������Ʈ�����)�������������� ����� 
        //���� ��Ʈ�� ��ũ��Ʈ ����� ���� �����տ� ��
        //������ ü���� 10�����̰� ���������� ����ü �߻�. ������ Ŭ���� �Ǹ� ����Ŭ�����гγ����� �ϸ��...
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
