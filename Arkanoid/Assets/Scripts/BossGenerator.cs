using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    public GameObject BossPrefab;  //보스의 프리팹

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossSpawn(Vector2 SpawnPos)
    {
        GameObject a_Boss = Instantiate(BossPrefab, SpawnPos, Quaternion.identity); //보스 소환
    }
}
