using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // The player's score
    public Text scoreText; 

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