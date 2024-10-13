using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)  //����, ũ��
    {
        Vector3 OrgPos = transform.localPosition;   //�� ����

        float time = 0.0f;
        while (time < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;  //���°�
            float y = Random.Range(-1f, 1f) * magnitude;  //���°�

            transform.localPosition = new Vector3(OrgPos.x + x, OrgPos.y + y, OrgPos.z);  //����

            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = OrgPos; // ���� ��ġ�� ����
    }
}
