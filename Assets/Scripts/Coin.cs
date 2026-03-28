using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 1; // 1 = bronze, 3 = silver, 5 = gold

    [Header("Audio")]
    public AudioClip coinSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.instance != null)
                ScoreManager.instance.AddScore(scoreValue);
            if (coinSound != null)
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
            Destroy(gameObject);
        }
    }
}
