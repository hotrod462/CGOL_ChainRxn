using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public LevelManager levelManager;

    public void StartGame()
    {
        levelManager.LoadLevel(0); // Load the first level
    }

    public void NextLevel()
    {
        levelManager.LoadLevel(1); // Load the next level
    }
}