using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalState : GameStates
{
    public override void EnterState(GameStateManager gameStateManager) 
    {
        Debug.Log("GoalState not implemented yet.");
        gameStateManager.SwitchState(gameStateManager.countdownState);
    }

    public override void UpdateState(GameStateManager gameStateManager) { }
}
