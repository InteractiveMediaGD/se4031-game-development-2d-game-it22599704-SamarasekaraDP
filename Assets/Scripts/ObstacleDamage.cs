using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    public int damage = 25;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
            ph.TakeDamage(damage);
    }
}