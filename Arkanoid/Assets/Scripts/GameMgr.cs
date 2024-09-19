using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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

    [Header("���ӿ���")]
    public GameObject GameOverPanel;
    public Text GameOverResultText;


    [Header("����Ŭ����")]
    public GameObject GameClearPanel;
    public Text GameClearResultText;
    private int TotalBlocks;          // �� ��� ��


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

    public void ClearPanel()
    {
        Time.timeScale = 0;   //�Ͻ�����
        GameClearPanel.gameObject.SetActive(true);       //Ų��
        GameClearResultText.text = "<color=#FF0000>" + Score.ToString() + "</color>";  //�÷�����
        
    }


    void GameOver()
    {
        Time.timeScale = 0;            //�Ͻ�����
        GameOverPanel.gameObject.SetActive(true);         //Ų��.
        GameOverResultText.text = "<color=#FF0000>" + Score.ToString() + "</color>";  //�÷�����
    }
}
