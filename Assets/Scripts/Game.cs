using System.Collections;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour
{
    Button[,] buttons;
    Image[] images;
    Lines lines;
    public GameOverScreen GameOverScreen;
    public static float timeStart = 10;
    public static int gameScore=0;
    public Text timerText,scoreText;

    void Start()
    {
        timerText.text=timeStart.ToString();
        lines = new Lines(ShowBox);
        InitButtons();
        InitImages();
        lines.Start();
        countDownTimer();
    }
    public void ShowBox(int x, int y, int ball)
    {
        buttons[x,y].GetComponent<Image>().sprite = images[ball].sprite;
    }
    public void Click()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        int nr = GetNumber(name);
        int x= nr % Lines.SIZE;
        int y = nr / Lines.SIZE;
        lines.Click(x,y);
    }
    private void InitButtons()
    {
        buttons=new Button[Lines.SIZE,Lines.SIZE];
        for (int nr=0; nr < Lines.SIZE * Lines.SIZE;nr++)
            buttons[nr % Lines.SIZE, nr / Lines.SIZE] = GameObject.Find($"Button ({nr})").GetComponent<Button>();
       
    }
    private void InitImages ()
    {
        images = new Image[Lines.BALLS];
        for (int j = 0; j < Lines.BALLS; j++)
            images[j]=GameObject.Find($"Image ({j})").GetComponent<Image>();
    }

    private int GetNumber (string name)
    {
       Regex regex = new Regex("\\((\\d+)\\)"); 
       Match match = regex.Match (name);
       if (!match.Success)
            throw new System.Exception("Unrecognized object name");
       Group group = match.Groups[1]; 
       string number = group.Value;
       return Convert.ToInt32(number);
    }


    void countDownTimer()
    {
        if (timeStart > 0)
        {
            timeStart--;
            Invoke("countDownTimer",1.0f);
        }
        else
            GameOverScreen.Setup(gameScore);
    }

    void Update() 
    {
        timerText.text=Mathf.Round(timeStart).ToString();
        scoreText.text=gameScore.ToString();
    }
}
