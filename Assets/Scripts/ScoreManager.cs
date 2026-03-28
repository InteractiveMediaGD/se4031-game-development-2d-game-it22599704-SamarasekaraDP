using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake() { instance = this; }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void ResetScore()
    {
        score = 0;
        if (scoreText != null)
            scoreText.text = "Score: 0";
    }
}
