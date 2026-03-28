using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 18f;
    public float maxSpeed = 10f;
    public float speedIncreaseRate = 0.02f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    private bool isDead = false;
    private Animator anim;

    [Header("Audio")]
    public AudioClip jumpSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead) return;

        // Speed increase over time
        if (moveSpeed < maxSpeed)
            moveSpeed += speedIncreaseRate * Time.deltaTime;

        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, groundCheckRadius, groundLayer);

        // Left / Right movement
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);

        // Flip sprite based on direction
        if (h > 0.1f) transform.localScale = new Vector3(1, 1, 1);
        else if (h < -0.1f) transform.localScale = new Vector3(-1, 1, 1);

        // Jump
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if (audioSource != null && jumpSound != null)
                audioSource.PlayOneShot(jumpSound);
        }

        // Drive Animator
        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            anim.SetBool("IsGrounded", isGrounded);
        }
    }

    public void SetDead(bool dead)
    {
        isDead = dead;
        if (anim != null) anim.SetFloat("Speed", 0f);
    }
    public void ResetSpeed() { moveSpeed = 5f; }
}