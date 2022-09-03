using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionState : GameState
{
    GameState invoke() {
        if (MovementHandler.tilesHaveBeenSelected() && MovementHandler.matchSuccessful() ){
            return new DeletionState();
        }    
        else if (MovementHandler.tileHasBeenClicked()) {
            return new InitState();
        }
    }
}