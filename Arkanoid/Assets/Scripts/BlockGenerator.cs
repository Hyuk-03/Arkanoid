using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlockGenerator : MonoBehaviour
{
    public GameObject BlockPrefab;     //프리팹
    public Sprite[] BlocksColor;       //벽돌의 색깔.
    [SerializeField] Vector2 Pos;      //벽돌 생성 시작 위치
    [SerializeField] Vector2 Offset;   //벽돌 간격
    public string[] tags;              //벽돌의 첫 색깔 인덱스의 이름을 바꾸기 위한.
                                      
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
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {   //이건 한번에 생성(색깔은 없음)
                //Instantiate(BlockPrefab, new Vector2(Pos.x +(j * Offset.x), Pos.y +(i * Offset.y)),Quaternion.identity);

                Vector2 position = new Vector2(Pos.x + (j * Offset.x), Pos.y + (i * Offset.y));  // 블록 위치 계산

                GameObject block = Instantiate(BlockPrefab, position, Quaternion.identity);       // 블록 인스턴스화


                SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();  // SpriteRenderer 가져오기, 화면에 그리기 위해서
               
                if (spriteRenderer != null && BlocksColor.Length > 0)   //인덱스 설정
                {

                    //spriteRenderer.sprite = BlocksColor[Random.Range(0, BlocksColor.Length)];  // 랜덤 스프라이트 선택 및 설정 , 0인덱스~내가설정한길이.

                    // 각 행에 대해 색상을 순서대로 설정
                    int ColorIndex = i % BlocksColor.Length; // 색상 인덱스 결정
                    spriteRenderer.sprite = BlocksColor[ColorIndex]; // 색상 설정


                    //if (tags != null && tags.Length > 0)      //태그 설정
                    //{
                    //    if (ColorIndex < tags.Length)         //인덱스가 배열 범위를 넘지 않도록 확인
                    //    {
                    //        block.tag = tags[ColorIndex];       //게임 오브젝트에 태그 설정

                    //    }
                    //}

                    if(i == 0)
                    {
                        block.tag = "HardBlock";      //하드블럭태그설정
                        
                    }
                    else
                    {
                        block.tag = "Block";          //그외에는일반블럭
                    }

                }
            }
        }
    }

}
