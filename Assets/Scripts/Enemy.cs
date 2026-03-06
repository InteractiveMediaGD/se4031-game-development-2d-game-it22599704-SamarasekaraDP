using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float autoDestroyTime = 10f;
    public float damageAmount = 20f;

    void Start()
    {
        Destroy(gameObject, autoDestroyTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(damageAmount);
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Projectile"))
        {
            ScoreManager.Instance.AddScore(1);
            Destroy(gameObject);
            Destroy(other.gameObject);
            return;
        }
    }
}