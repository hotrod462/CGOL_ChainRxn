using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level_Easy");
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene("Level_Med");
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene("Level_Hard");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}