using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text highScore;
   public void PlayGame()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
   }

   public void ExitGame()
   {
        Application.Quit();
   }

   public void Update()
   {
        highScore.text = PlayerPrefs.GetInt("Highscore").ToString();
   }
}
