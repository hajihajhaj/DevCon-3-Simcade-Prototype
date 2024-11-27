using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // The player's score
    public TMP_Text scoreText; // Use TMP_Text instead of Text

    void Start()
    {
        UpdateScoreText(); // Initialize the score display
    }

    public void AddScore(int points)
    {
        score += points; // Increase the score
        UpdateScoreText(); // Refresh the score display
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogWarning("Score Text UI element is not assigned!");
        }
    }
}
