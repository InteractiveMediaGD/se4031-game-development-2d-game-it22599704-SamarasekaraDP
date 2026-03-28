using UnityEngine;

public class SpiritOrb : MonoBehaviour
{
    public float speed = 15f;
    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Die(true); // true = killed by projectile
            Destroy(gameObject);
        }
    }
}
