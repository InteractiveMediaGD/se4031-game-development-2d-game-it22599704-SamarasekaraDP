using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float healAmount = 25f;
    public float autoDestroyTime = 8f;

    void Start()
    {
        Destroy(gameObject, autoDestroyTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.Instance.Heal(healAmount);
            Destroy(gameObject);
        }
    }

}