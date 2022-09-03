using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionState : GameState
{
    GameState invoke() {
        MovmentHandler.deleteChosenTiles();
        return GenerateState();
    }
}