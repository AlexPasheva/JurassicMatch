using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitState : GameState
{
    private Timer timer = new Timer();
    GameState invoke() {
        if (Timer.RunsOut() | DinaurList.areGathered){
            return new GameOverState();
        }    
        else if (MovementHandler.tileHasBeenClicked()) {
            return new SelectionState();
        }
    }
}