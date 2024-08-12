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
        for(int i = 0; i < row; i++)
        {
            for(int j = 0; j < col; j++)
            {   //�̰� �ѹ��� ����(������ ����)
                //Instantiate(BlockPrefab, new Vector2(Pos.x +(j * Offset.x), Pos.y +(i * Offset.y)),Quaternion.identity);

                Vector2 position = new Vector2(Pos.x + (j * Offset.x), Pos.y + (i * Offset.y));  // ���� ��ġ ���
      
                GameObject block = Instantiate(BlockPrefab, position, Quaternion.identity);       // ���� �ν��Ͻ�ȭ


                SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();  // SpriteRenderer ��������, ȭ�鿡 �׸��� ���ؼ�
                if (spriteRenderer != null && BlocksColor.Length > 0)   //�ε��� ����
                {

                    //spriteRenderer.sprite = BlocksColor[Random.Range(0, BlocksColor.Length)];  // ���� ��������Ʈ ���� �� ���� , 0�ε���~���������ѱ���.

                    // �� �࿡ ���� ������ ������� ����
                    int colorIndex = i % BlocksColor.Length; // ���� �ε��� ����
                    spriteRenderer.sprite = BlocksColor[colorIndex]; // ���� ����
                }


            }
        }
    }
    
}