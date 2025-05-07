using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool isShaking = false;

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    public IEnumerator ShakeContinuous(float magnitude)
    {
        isShaking = true;
        Vector3 originalPosition = transform.localPosition;

        while (isShaking)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    public void StopShake()
    {
        isShaking = false;
    }
}
