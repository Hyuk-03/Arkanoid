using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlocksCtrl : MonoBehaviour
{
    public GameObject BlockPrefab;     //프리팹
    public Sprite[] Blocks;            //벽돌의 색깔.
    [SerializeField] Vector2 Pos;      //벽돌 생성 시작 위치
    [SerializeField] Vector2 Offset;   //벽돌 간격

   
    [SerializeField] int row;  //행
    [SerializeField] int col;  //열
    



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
            {   //이건 한번에 생성(색깔은 없음)
                //Instantiate(BlockPrefab, new Vector2(Pos.x +(j * Offset.x), Pos.y +(i * Offset.y)),Quaternion.identity);

                Vector2 position = new Vector2(Pos.x + (j * Offset.x), Pos.y + (i * Offset.y));  // 블록 위치 계산
      
                GameObject block = Instantiate(BlockPrefab, position, Quaternion.identity);       // 블록 인스턴스화


                SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();  // SpriteRenderer 가져오기, 화면에 그리기 위해서
                if (spriteRenderer != null && Blocks.Length > 0)   //인덱스 설정
                {
                   
                    spriteRenderer.sprite = Blocks[Random.Range(0, Blocks.Length)];  // 랜덤 스프라이트 선택 및 설정 , 0인덱스~내가설정한길이.
                }


            }
        }
    }
    
}
