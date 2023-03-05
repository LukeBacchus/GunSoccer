using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.CINEMATIC;

    public override void EnterState(GameStateManager gameStateManager) 
    {
        Debug.Log("GoalState not implemented yet.");

        if (gameStateManager.prevState is OvertimeState)
        {
            gameStateManager.SwitchState(gameStateManager.gameOverState);
        }
        else
        {
            gameStateManager.SwitchState(gameStateManager.countdownState);
        }
    }

    public override void UpdateState(GameStateManager gameStateManager) { }
}
