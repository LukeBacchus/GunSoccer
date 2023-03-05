using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.CINEMATIC;

    private GameStats gameStats;
    private TMPro.TextMeshProUGUI countDownText;
    private InitializeMap initMap;
    private GameObject soccerBall;

    public CountdownState(GameStats gameStats, TextMeshProUGUI countDownText, InitializeMap initMap, GameObject soccerBall)
    {
        this.gameStats = gameStats;
        this.countDownText = countDownText;
        this.initMap = initMap;
        this.soccerBall = soccerBall;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        initMap.ResetPlayerLocs(gameStateManager.players);
        soccerBall.transform.position = new Vector3(0, 5, 0);
        soccerBall.transform.rotation = Quaternion.identity;
        soccerBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        soccerBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameStateManager.StartCoroutine(Countdown(gameStateManager));
    }

    public override void UpdateState(GameStateManager gameStateManager) { }

    private IEnumerator Countdown(GameStateManager gameStateManager)
    {
        float countDownTime = 5.99f;
        while (countDownTime >= 0)
        {
            countDownTime -= Time.deltaTime;
            countDownText.text = ((int) countDownTime).ToString();
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
