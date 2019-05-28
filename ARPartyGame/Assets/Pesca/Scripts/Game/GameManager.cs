using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CupBehaviour cup;
    [SerializeField] int time;
    [SerializeField] UnityEngine.UI.Text scoreText;
    [SerializeField] GameObject canvasFinal;
    Timer gameTimer;

    private void Start()
    {
        gameTimer = new Timer();
        gameTimer.SetTime(time);
        gameTimer.StartTimer = true;
    }

    private void Update()
    {
        gameTimer.UpdateTime(Time.deltaTime);
        if (gameTimer.TimeFinish)
            FinalGame();
    }
    private void FinalGame()
    {
        scoreText.text = cup.score.ToString();
        canvasFinal.SetActive(true);
    }
}
