using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)  //지속, 크기
    {
        Vector3 OrgPos = transform.localPosition;   //값 저장

        float time = 0.0f;
        while (time < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;  //흔드는값
            float y = Random.Range(-1f, 1f) * magnitude;  //흔드는값

            transform.localPosition = new Vector3(OrgPos.x + x, OrgPos.y + y, OrgPos.z);  //흔들기

            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = OrgPos; // 원래 위치로 복귀
    }
}
