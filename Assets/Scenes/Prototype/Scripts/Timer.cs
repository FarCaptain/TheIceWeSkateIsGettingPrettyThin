using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float maxTime;
    private float currentTime = 0f;
    private bool timerRunning = false;
    private bool timerStart;
    private float timerCompletionRate;
    private bool reverseTimer = false;

    public float MaxTime
    {
        get
        {
            return maxTime;
        }
        set
        {
            maxTime = value;
        }
    }

    public float CurrentTime
    {
        get
        {
            return currentTime;
        }
        set
        {
            currentTime = value;
        }
    }

    public bool TimerStart
    {
        set
        {
            timerStart = value;
        }
        get
        {
            return timerStart;
        }
    }

    public bool TimerRunning
    {
        set
        {
            timerRunning = value;
        }
        get
        {
            return timerRunning;
        }
    }

    public float TimerCompletionRate
    {
        get
        {
            return timerCompletionRate;
        }
    }

    public bool ReverseTimer
    {
        get
        {
            return reverseTimer;
        }
        set
        {
            reverseTimer = value;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timerStart)
        {
            timerRunning = true;
            if (reverseTimer == false)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= maxTime)
                {
                    timerRunning = false;
                    timerStart = false;
                    currentTime = 0;
                    timerCompletionRate = 0;
                }
            }
            else if (reverseTimer == true)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    timerRunning = false;
                    timerStart = false;
                    currentTime = 0;
                    timerCompletionRate = 0;
                }
            }
            timerCompletionRate = Mathf.Clamp01(currentTime / maxTime);
        }

    }

    public void ResetTimer()
    {
        currentTime = 0f;
    }
}
