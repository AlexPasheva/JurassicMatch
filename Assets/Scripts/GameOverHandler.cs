using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{

    void Start()
    {

    }

    private void Win()
    {
        Debug.Log("Congratulations, you won");
    }

    private void Lose()
    {
        Debug.Log("Sorry, you lost");
    }

    private void OnEnable()
    {
        DinosaurCollector.gameOver += Win;
        Timer.gameOver += Lose;
    }

    private void OnDisable()
    {
        DinosaurCollector.gameOver -= Win;
        Timer.gameOver += Lose;
    }
}
