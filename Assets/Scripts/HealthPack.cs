using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 25;

    [Header("Audio")]
    public AudioClip healthSound;

    void Start()
    {
        Destroy(gameObject, 8f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Heal(healAmount);
            if (healthSound != null)
                AudioSource.PlayClipAtPoint(healthSound, transform.position);
            Destroy(gameObject);
        }
    }
}
