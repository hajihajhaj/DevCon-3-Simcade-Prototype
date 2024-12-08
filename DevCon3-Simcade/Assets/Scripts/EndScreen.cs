using UnityEngine;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText; // Assign this in the Inspector

    void Start()
    {
        // Get the saved score from PlayerPrefs
        float finalScore = PlayerPrefs.GetFloat("FinalScore", 0);

        // Display the score
        finalScoreText.text = "Final Score: " + finalScore.ToString("0");

        Cursor.visible = true;
    }
}
