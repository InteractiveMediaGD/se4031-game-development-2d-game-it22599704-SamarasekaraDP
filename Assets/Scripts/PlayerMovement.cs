using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float topBoundary = 4.5f;
    public float bottomBoundary = -4.5f;

    private Rigidbody2D rb;
    private bool gameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameOver) return;

        float move = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            move = 1f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            move = -1f;

        rb.velocity = new Vector2(0f, move * moveSpeed);

        float clampedY = Mathf.Clamp(transform.position.y, bottomBoundary, topBoundary);
        transform.position = new Vector3(transform.position.x, clampedY, 0);
    }

    public void SetGameOver()
    {
        gameOver = true;
        rb.velocity = Vector2.zero;
    }
}