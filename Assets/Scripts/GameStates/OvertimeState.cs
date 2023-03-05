using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertimeState : GameStates
{
    private GameStats gameStats;
    private TMPro.TextMeshProUGUI timerText;

    public OvertimeState(GameStats gameStats, TMPro.TextMeshProUGUI timerText)
    {
        this.gameStats = gameStats;
        this.timerText = timerText;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        timerText.text = "Overtime!";
    }

    public override void UpdateState(GameStateManager gameStateManager) 
    {
        if (gameStats.teamOneScore != gameStats.teamTwoScore)
        {
            gameStateManager.SwitchState(gameStateManager.gameOverState);
        }
    }
}
