using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : GameStates
{
    public override void EnterState(GameStateManager gameStateManager) 
    {
        Debug.Log("IntroState not implemented yet.");
        gameStateManager.SwitchState(gameStateManager.countdownState);
    }
    public override void UpdateState(GameStateManager gameStateManager) { }
}
