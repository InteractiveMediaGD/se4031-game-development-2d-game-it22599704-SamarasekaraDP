using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 12f;
    public float lifeTime = 2f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("ScoreTrigger")) return;
        if (other.CompareTag("HealthPack")) return;
        Destroy(gameObject);
    }
}