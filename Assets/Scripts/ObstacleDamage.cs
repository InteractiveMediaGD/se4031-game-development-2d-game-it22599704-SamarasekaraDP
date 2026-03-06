using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    public float damageAmount = 25f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(damageAmount);
        }
    }
}