using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCtrl : MonoBehaviour
{
    int Hp = 1;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(int a_Value)
    {
        if (Hp <= 0)
            return;

        Hp -= a_Value;
        if (Hp <= 0)
            Hp = 0;

        StartCoroutine(Break());
    }

    IEnumerator Break()
    {
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
}
