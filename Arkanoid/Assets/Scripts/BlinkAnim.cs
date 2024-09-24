using UnityEngine;
using UnityEngine.UI;

public class BlinkAnim : MonoBehaviour
{
    private float time;            //시간
    private Text BlinkText;       //텍스트
    public float BlinkTime = 1f; // 한 사이클의 지속시간

    void Start()
    {
        BlinkText = GetComponent<Text>(); // Text 컴포넌트 가져오기
    }

    void Update()
    {
        time += Time.deltaTime;
        float Alpha = Mathf.PingPong(time / BlinkTime, 1f); // 알파값 계산

        // 알파값을 텍스트 색상에 적용
        Color Color = BlinkText.color;
        Color.a = Alpha;
        BlinkText.color = Color;
    }
}
