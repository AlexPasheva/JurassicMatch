using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    enum State
    {
        INITIALIZED,
        ACTIVE,
        STOPPED,
        PAUSED
    }

    public static event Action gameOver;
    public Slider timerSlider;

    private State currentState = State.INITIALIZED;

    [SerializeField] private int pauseTimeout;
    [SerializeField] private int startSeconds;

    [SerializeField] private Text currentTimeText;

    private float currentTime;
    private float timeInPausedState = 0;

    void Start()
    {
        timerSlider.maxValue = startSeconds;
        currentTime = startSeconds;
        timerSlider.value = currentTime;
        Activate();
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

        timerSlider.value = currentTime;

        if (HasRunOut())
        {
            gameOver?.Invoke();
        }
    }

    public bool HasRunOut()
    {
        return currentTime <= 0;
    }

    private void ActiveStateHandler()
    {
        currentTime -= Time.deltaTime;

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

    public void Increment(float incrementTime)
    {
        currentTime += incrementTime;
    }

    public void Stop()
    {
        if (currentState == State.ACTIVE)
        {
            currentState = State.STOPPED;
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

    private void OnEnable()
    {
        Selector.incrementTime += Increment;
    }

    private void OnDisable()
    {
        Selector.incrementTime -= Increment;
    }

}
