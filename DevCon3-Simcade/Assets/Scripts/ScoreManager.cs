using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float currentScore = 0f; // The player's current score
    public float scoreIncreaseRate = 10f; // How fast the score increases (per second)

    [SerializeField] TextMeshProUGUI scoreText; // The on-screen score display

    void Start()
    {
        // Initialize the score
        currentScore = 0f;
    }

    void Update()
    {
        // Increase the score over time
        currentScore += scoreIncreaseRate * Time.deltaTime;

        // Update the score on the screen
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString("0"); // Display as a whole number
        }
    }

    public void SaveScoreAndEndGame()
    {
        // Save the score to PlayerPrefs
        PlayerPrefs.SetFloat("FinalScore", currentScore);
        PlayerPrefs.Save(); // Ensure the score is saved immediately

        // Load the end screen
        SceneManager.LoadScene("EndScreen");
    }
}
