using UnityEngine;

public class PlatformLanding : MonoBehaviour
{
    private bool hasScored = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (hasScored) return;
        if (!col.gameObject.CompareTag("Player")) return;

        hasScored = true;
        ScoreManager.instance.AddScore(1);
        Debug.Log("SCORE ADDED. Total: " + ScoreManager.instance.score);
    }
}