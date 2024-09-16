using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlockGenerator : MonoBehaviour
{
    public GameObject BlockPrefab;     //������
    public Sprite[] BlocksColor;       //������ ����.
    [SerializeField] Vector2 Pos;      //���� ���� ���� ��ġ
    [SerializeField] Vector2 Offset;   //���� ����
    public string[] tags;              //������ ù ���� �ε����� �̸��� �ٲٱ� ����.
                                      
    [SerializeField] int row;  //��
    [SerializeField] int col;  //��

    // Start is called before the first frame update
    void Start()
    {
        
        CreateBlocks();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateBlocks()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {   //�̰� �ѹ��� ����(������ ����)
                //Instantiate(BlockPrefab, new Vector2(Pos.x +(j * Offset.x), Pos.y +(i * Offset.y)),Quaternion.identity);

                Vector2 position = new Vector2(Pos.x + (j * Offset.x), Pos.y + (i * Offset.y));  // ��� ��ġ ���

                GameObject block = Instantiate(BlockPrefab, position, Quaternion.identity);       // ��� �ν��Ͻ�ȭ


                SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();  // SpriteRenderer ��������, ȭ�鿡 �׸��� ���ؼ�
               
                if (spriteRenderer != null && BlocksColor.Length > 0)   //�ε��� ����
                {

                    //spriteRenderer.sprite = BlocksColor[Random.Range(0, BlocksColor.Length)];  // ���� ��������Ʈ ���� �� ���� , 0�ε���~���������ѱ���.

                    // �� �࿡ ���� ������ ������� ����
                    int ColorIndex = i % BlocksColor.Length; // ���� �ε��� ����
                    spriteRenderer.sprite = BlocksColor[ColorIndex]; // ���� ����


                    //if (tags != null && tags.Length > 0)      //�±� ����
                    //{
                    //    if (ColorIndex < tags.Length)         //�ε����� �迭 ������ ���� �ʵ��� Ȯ��
                    //    {
                    //        block.tag = tags[ColorIndex];       //���� ������Ʈ�� �±� ����

                    //    }
                    //}

                    if(i == 0)
                    {
                        block.tag = "HardBlock";      //�ϵ���±׼���
                        
                    }
                    else
                    {
                        block.tag = "Block";          //�׿ܿ����Ϲݺ�
                    }

                }
            }
        }
    }

}
