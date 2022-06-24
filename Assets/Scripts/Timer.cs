using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    enum State
    {
        INITIALIZED,
        ACTIVE,
        STOPPED,
        PAUSED
    }
    private State currentState = State.INITIALIZED;

    [SerializeField] private int pauseTimeout;
    [SerializeField] private int startSeconds;
    [SerializeField] private int increasedTimerSeconds;

    [SerializeField] private Slider timerSlider;
    [SerializeField] private Text currentTimeText;

    private float currentTime;
    private float timeInPausedState = 0;

    void Start()
    {
        currentTime = startSeconds;
        timerSlider.maxValue = startSeconds;
        timerSlider.value = startSeconds;
    }

    public void Initialise(int startingTime)
    {
        currentTime = startingTime;
        currentState = State.INITIALIZED;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.INITIALIZED:
                currentTime = startSeconds;
                break;
            case State.ACTIVE:
                ActiveStateHandler();
                break;
            case State.PAUSED:
                PausedStateHandler();
                break;
            case State.STOPPED:
                break;
            default:
                throw new NullReferenceException();
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        PrintTime(time);

    }
    private void ActiveStateHandler()
    {
        currentTime -= Time.deltaTime;
        
        timerSlider.value = currentTime;

        if (currentTime <= 0f)
        {
            currentState = State.INITIALIZED;
        }
    }
    private void PausedStateHandler()
    {
        timeInPausedState += Time.deltaTime;

        if (timeInPausedState >= pauseTimeout)
        {
            currentState = State.ACTIVE;
            timeInPausedState = 0;
        }
    }
    public void Stop()
    {
        if (currentState == State.ACTIVE)
        {
            currentState = State.STOPPED;
        }
    }

    public void Increase(int timerIncreaseSeconds)
    {
        currentTime += timerIncreaseSeconds;
        if (currentTime > startSeconds)
        {
            currentTime = startSeconds;
        }
    }

    public void Activate()
    {
        if (currentState == State.INITIALIZED || currentState == State.STOPPED)
        {
            currentState = State.ACTIVE;
        }
    }

    public void Pause()
    {
        if (currentState == State.ACTIVE)
        {
            currentState = State.PAUSED;
        }

    }

    public void Reset()
    {
        currentState = State.INITIALIZED;
    }

    private void PrintTime(TimeSpan time)
    {
        if (time.Seconds < 10)
        {
            currentTimeText.text = $"{time.Minutes.ToString()}:0{time.Seconds.ToString()}";
        }
        else
        {
            currentTimeText.text = $"{time.Minutes.ToString()}:{time.Seconds.ToString()}";
        }
    }

    
    public float CurrentTime
    {
        get { return currentTime; }
    }

}
