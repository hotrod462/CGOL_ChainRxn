using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        // Display the player's final score
        scoreText.text = "Final Score: " + GameData.Instance.playerScore;
    }

    public void RestartGame()
    {
        // Load the game scene to restart the game
        FindObjectOfType<GameController>().LoadScene("GameScene");
    }

    public void MainMenu()
    {
        // Load the main menu scene
        FindObjectOfType<GameController>().LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        // Quit the game application
        Application.Quit();
    }
}