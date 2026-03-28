using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float walkSpeed = 2f;
    public int damage = 25;
    private Transform player;
    private bool isDead = false;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
        Destroy(gameObject, 15f);
    }

    void Update()
    {
        if (isDead || player == null) return;

        float dir = player.position.x > transform.position.x ? 1f : -1f;

        // Ledge detection: shoot a raycast down from slightly ahead of the enemy
        // The 0.6f offset is how far ahead to check. The 1.5f is how far down to check.
        Vector2 rayStart = new Vector2(transform.position.x + (dir * 0.6f), transform.position.y);
        RaycastHit2D groundInfo = Physics2D.Raycast(rayStart, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));
        
        // This draws a red line in your Scene view so you can visually see the edge check!
        Debug.DrawRay(rayStart, Vector2.down * 1.5f, Color.red);

        if (groundInfo.collider == null)
        {
            // No ground ahead (a gap is coming up). Stop moving!
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (anim != null) anim.SetFloat("Speed", 0f);
        }
        else
        {
            // Ground is present. Keep walking towards the player.
            rb.velocity = new Vector2(dir * walkSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-dir, 1, 1); // Flip sprite
            if (anim != null) anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        bool stomped = col.contacts[0].normal.y < -0.5f;
        if (stomped)
        {
            Die(false);
        }
        else
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Die(false);
        }
    }

    public void Die(bool byProjectile)
    {
        if (isDead) return;
        isDead = true;
        if (byProjectile && ScoreManager.instance != null)
            ScoreManager.instance.AddScore(2);
        Destroy(gameObject);
    }
}