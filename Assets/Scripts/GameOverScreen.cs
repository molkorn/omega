using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;

    
    public void Setup(int score)
        {
            gameObject.SetActive(true);
            pointsText.text = score.ToString()+ " POINTS";
            if (score > PlayerPrefs.GetInt("Highscore", 0))
            {
                PlayerPrefs.SetInt("Highscore", score);

            }
        
        }
    
    public void ExitMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}  

