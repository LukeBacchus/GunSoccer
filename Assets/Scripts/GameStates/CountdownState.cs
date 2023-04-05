using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.PREGAME;

    private GameStats gameStats;
    private TMPro.TextMeshProUGUI countDownText;
    private InitializeMap initMap;
    private SoccerBallBehavior soccerBallBehavior;

    public CountdownState(GameStats gameStats, TextMeshProUGUI countDownText, InitializeMap initMap, SoccerBallBehavior soccerBallBehavior)
    {
        this.gameStats = gameStats;
        this.countDownText = countDownText;
        this.initMap = initMap;
        this.soccerBallBehavior = soccerBallBehavior;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        initMap.ResetPlayerLocs(gameStateManager.players);
        soccerBallBehavior.DisableGravity();
        soccerBallBehavior.ResetBall();
        gameStateManager.StartCoroutine(Countdown(gameStateManager));
    }

    public override void UpdateState(GameStateManager gameStateManager) 
    {
        if (Input.GetButtonDown("Menu"))
        {
            gameStateManager.SwitchState(gameStateManager.pauseState);
        }
    }

    private IEnumerator Countdown(GameStateManager gameStateManager)
    {
        float countDownTime = 5.99f;
        while (countDownTime >= 0)
        {
            countDownTime -= Time.deltaTime;
            countDownText.text = ((int) countDownTime).ToString();
            yield return null;
        }

        soccerBallBehavior.EnableGravity();
        countDownText.text = "";

        gameStateManager.SwitchState(GetNextState(gameStateManager));
    }

    private GameStates GetNextState(GameStateManager gameStateManager)
    {
        if (!gameStats.TimeIsUp())
        {
            return gameStateManager.ongoingGameState;
        }

        return gameStateManager.overtimeState;
    }
}
