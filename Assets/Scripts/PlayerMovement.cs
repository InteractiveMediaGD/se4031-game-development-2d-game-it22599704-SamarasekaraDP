using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 12f;
    public float topBoundary = 5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool gameOver = false;
    private bool isGrounded = false;
    private CircleCollider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (gameOver) return;

        // Check if player is standing on ground
        isGrounded = Physics2D.CircleCast(
            col.bounds.center,
            col.radius * 0.9f,
            Vector2.down,
            0.1f,
            groundLayer);

        // Jump only when on ground
        if ((Input.GetKeyDown(KeyCode.Space) ||
             Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Stop going above screen
        if (transform.position.y > topBoundary)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            transform.position = new Vector3(
                transform.position.x, topBoundary, 0);
        }
    }

    public void SetGameOver()
    {
        gameOver = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
    }

    public bool IsGrounded() { return isGrounded; }
}