using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(int levelIndex)
    {
        switch (levelIndex)
        {
            case 0:
                SceneManager.LoadScene("Level_Easy");
                break;
            case 1:
                SceneManager.LoadScene("Level_Medium");
                break;
            case 2:
                SceneManager.LoadScene("Level_Hard");
                break;
        }
    }
}