using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    
    public int playerScore; // This will hold the player's score

    void Awake()
    {
        // Ensure that there is only one instance of GameData
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
}