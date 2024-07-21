using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    void Start()
    {
        // Initialize player score
        GameData.Instance.playerScore = 0; // Set initial score
    }

    public void SetScore(int amount)
    {
        GameData.Instance.playerScore = amount;
        Debug.Log("Player Score: " + GameData.Instance.playerScore);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Load the specified scene
    }
}