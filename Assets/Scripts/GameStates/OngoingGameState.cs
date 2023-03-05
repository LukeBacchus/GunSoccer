using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OngoingGameState : GameStates
{
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
                gameStateManager.SwitchState(gameStateManager.countdownState);
            }
            else
            {
                gameStateManager.SwitchState(gameStateManager.gameOverState);
            }
        }
    }
}
