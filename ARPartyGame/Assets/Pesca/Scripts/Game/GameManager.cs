using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CupBehaviour cup;
    [SerializeField] int time;
    [SerializeField] UnityEngine.UI.Text scoreText;
    [SerializeField] GameObject canvasFinal;
    [SerializeField] UnityEngine.UI.Text timeText;
    [SerializeField] SushiController controller;


    Timer gameTimer;

    private void Start()
    {
        gameTimer = new Timer();
        gameTimer.SetTime(time);
        gameTimer.StartTimer = true;
    }

    private void Update()
    {
        if(controller.tutorial.TimeFinish)
        {
            gameTimer.UpdateTime(Time.deltaTime);
            if (gameTimer.TimeFinish)
                FinalGame();
            else
                UpdateUITime();
        }
        
    }
    private void FinalGame()
    {
        scoreText.text = cup.score.ToString();
        canvasFinal.SetActive(true);
    }
    private void UpdateUITime()
    {
        timeText.text = ((int) gameTimer.CurrentTime+1).ToString();
    }

}
