using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCtrl : MonoBehaviour
{
    public float MoveSpeed = 10.0f;
    float h = 0.0f;
    Vector3 PaddlePos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
      PaddlePos = transform.position; 
    }

   



    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        if (h != 0.0f)
        {
            if (PaddlePos != null)
            {
                PaddlePos.x += h * MoveSpeed * Time.deltaTime;
                PaddlePos.x = Mathf.Clamp(PaddlePos.x, -3.52f, 3.52f);
                transform.position = PaddlePos;
            }

        }
        
    }

}
