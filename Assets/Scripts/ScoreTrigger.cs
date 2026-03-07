using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool scored = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (scored) return;
        if (!other.CompareTag("Player")) return;

        // Only score if player is ABOVE the wall center
        if (other.transform.position.y > transform.parent.position.y)
        {
            scored = true;
            ScoreManager.Instance.AddScore(1);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) scored = false;
    }
}