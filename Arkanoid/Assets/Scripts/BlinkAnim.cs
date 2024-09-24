using UnityEngine;
using UnityEngine.UI;

public class BlinkAnim : MonoBehaviour
{
    private float time;            //�ð�
    private Text BlinkText;       //�ؽ�Ʈ
    public float BlinkTime = 1f; // �� ����Ŭ�� ���ӽð�

    void Start()
    {
        BlinkText = GetComponent<Text>(); // Text ������Ʈ ��������
    }

    void Update()
    {
        time += Time.deltaTime;
        float Alpha = Mathf.PingPong(time / BlinkTime, 1f); // ���İ� ���

        // ���İ��� �ؽ�Ʈ ���� ����
        Color Color = BlinkText.color;
        Color.a = Alpha;
        BlinkText.color = Color;
    }
}
