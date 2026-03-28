using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    public Image overlayImage;
    public float flashDuration = 0.4f;

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        Color c = overlayImage.color;
        c.a = 0.35f;
        overlayImage.color = c;
        overlayImage.gameObject.SetActive(true);

        float elapsed = 0f;
        while (elapsed < flashDuration)
        {
            c.a = Mathf.Lerp(0.35f, 0f, elapsed / flashDuration);
            overlayImage.color = c;
            elapsed += Time.deltaTime;
            yield return null;
        }
        overlayImage.gameObject.SetActive(false);
    }
}
