using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenUI : MonoBehaviour
{      
    // Start is called before the first frame update
    public Text score_text;
    public Text highscore_text;
    public Text wintext;
    
    void Start()
    {
        wintext.text = "YOU WIN!!!";
        if (UIManager.Instance != null)
        {
        int score = UIManager.Instance.score;
        int highscore = 0;

        score_text.text = "Score : " + score;
        highscore_text.text = "Lowest score : " + highscore;
        }
    }

    // Update is called once per frame
    void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
