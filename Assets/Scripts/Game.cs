using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Timer timer;
    private GameState state;
    private GridManager grid;

    public void Start()
    {
        timer = new Timer();
        timer.
        grid = new GridManager.GenerateGrid();

        state = new InitState();
        state.invoke();
    }

    public void Update()
    {
        switch (state) 
        {
            case GameState.Init:
                state = invoke();
                break;
            case GameState.Selection:
                state = invoke();
                break;
            case GameState.Deletion:
                state = invoke();
                break;
            case GameState.Generate:
                state = invoke();
                break;
            case GameState.GameOver:
                state = invoke();
                break;
            case GameState.Stalemate:
                state = invoke();
                break;
            default:
                break;
            }
    }
}