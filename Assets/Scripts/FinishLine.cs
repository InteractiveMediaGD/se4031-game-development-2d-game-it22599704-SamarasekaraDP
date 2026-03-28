using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject winPanel; // assign in Inspector (Stage 3 adds the Win Panel UI)

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;
            Debug.Log("LEVEL COMPLETE!");
            if (winPanel != null)
                winPanel.SetActive(true);
        }
    }
}
