using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        // Find the GameController object in the scene
        gameController = FindObjectOfType<GameController>();
    }

    public void PlayLevel1()
    {
        gameController.LoadScene("Level_Easy");
    }

    public void PlayLevel2()
    {
        gameController.LoadScene("Level_Med");
    }

    public void PlayLevel3()
    {
        gameController.LoadScene("Level_Hard");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}