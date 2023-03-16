using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OngoingGameState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.INGAME;

    private GameStats gameStats;

    public OngoingGameState(GameStats gameStats)
    {
        this.gameStats = gameStats;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {

    }

    public override void UpdateState(GameStateManager gameStateManager) 
    {
        gameStats.gameTime -= Time.deltaTime;
        gameStats.UpdateTimerUI();

        if (gameStats.TimeIsUp())
        {
            gameStateManager.SwitchState(gameStateManager.postGameState);
        }
    }
}
