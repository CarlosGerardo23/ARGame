using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer 
{
    private bool startTimer=false;
    private float currentTime=-1;
    private bool timeFinish = false;

    public bool TimeFinish { get { return timeFinish; } set { timeFinish = value; } }
    public bool StartTimer { get { return startTimer; } set { startTimer = value; } }
    public float CurrentTime { get { return currentTime; } }
 
    
    public void UpdateTime(float restTime)
    {
        if(startTimer)
        {
            currentTime -= restTime;
            if (currentTime < 0)
            {
                TimeOver();
            }
        }
    }

    public void SetTime(float startTime)
    {
        currentTime = startTime;
        timeFinish = false;
    }

    public void RestartTime(float newTime)
    {
        currentTime = newTime;
        startTimer = true;
        timeFinish = false;
    }
    void TimeOver()
    {
        startTimer = false;
        timeFinish = true;
    }
}
