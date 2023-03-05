using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OngoingGameState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.INGAME;

    private GameStats gameStats;
    private TMPro.TextMeshProUGUI timerText;

    public OngoingGameState(GameStats gameStats, TextMeshProUGUI timerText)
    {
        this.gameStats = gameStats;
        this.timerText = timerText;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {

    }

    public override void UpdateState(GameStateManager gameStateManager) 
    {
        gameStats.gameTime -= Time.deltaTime;
        timerText.text = "Timer: " + string.Format("{0:0}:{1:00}", Mathf.FloorToInt(gameStats.gameTime / 60), Mathf.FloorToInt(gameStats.gameTime % 60));

        if (gameStats.gameTime <= 0)
        {
            if (gameStats.teamOneScore == gameStats.teamTwoScore)
            {
                timerText.text = "Overtime!";
                gameStateManager.SwitchState(gameStateManager.countdownState);
            }
            else
            {
                timerText.text = "Times Up!";
                gameStateManager.SwitchState(gameStateManager.gameOverState);
            }
        }
    }

    private void OnGoal(GameStateManager gameStateManager)
    {
        gameStateManager.SwitchState(gameStateManager.goalState);
    }
}
