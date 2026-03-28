using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.15f;
    private Vector3 originalPos;

    void Start() { originalPos = transform.position; }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            transform.position = originalPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }
}