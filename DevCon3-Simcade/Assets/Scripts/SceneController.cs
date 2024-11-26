using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor; // Required for stopping Play Mode in the Editor
#endif

public class SceneController : MonoBehaviour
{
    // This function is called when the Play button is clicked
    public void PlayGame()
    {
        // Replace "GameScene" with the name of your gameplay scene
        SceneManager.LoadScene("Game");
    }

    // This function is called when the Help button is clicked
    public void OpenHelp()
    {
        // Replace "HelpScene" with the name of your help or tutorial scene
        SceneManager.LoadScene("Help");
    }

    // This function is called when the Exit button is clicked
    public void ExitGame()
    {
#if UNITY_EDITOR
        // Stop playing the game in the Unity Editor
        EditorApplication.isPlaying = false;
#else
        // Exit the application in a built game
        Application.Quit();
#endif
        Debug.Log("Game is exiting...");
    }
}
