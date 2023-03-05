using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownState : GameStates
{
    private GameStats gameStats;
    private TMPro.TextMeshProUGUI countDownText;

    public CountdownState(GameStats gameStats, TextMeshProUGUI countDownText)
    {
        this.gameStats = gameStats;
        this.countDownText = countDownText;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        gameStateManager.StartCoroutine(Countdown(gameStateManager));
    }

    public override void UpdateState(GameStateManager gameStateManager) { }

    private IEnumerator Countdown(GameStateManager gameStateManager)
    {
        float countDownTime = 5;
        while (countDownTime >= 0)
        {
            countDownTime -= Time.deltaTime;
            countDownText.text = Mathf.Ceil(countDownTime).ToString();
            yield return null;
        }

        countDownText.text = "";

        gameStateManager.SwitchState(GetNextState(gameStateManager));
    }

    private GameStates GetNextState(GameStateManager gameStateManager)
    {
        if (gameStats.gameTime > 0)
        {
            return gameStateManager.ongoingGameState;
        }

        return gameStateManager.overtimeState;
    }
}
