using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.CINEMATIC;

    private GameStats gameStats;
    private SoccerBallBehavior soccerBallBehavior;

    public GoalState(GameStats gameStats, SoccerBallBehavior soccerBallBehavior)
    {
        this.gameStats = gameStats;
        this.soccerBallBehavior = soccerBallBehavior;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        gameStateManager.StartCoroutine(GoalSlowMo(gameStateManager));
    }

    public override void UpdateState(GameStateManager gameStateManager) { }

    private void SlowMo()
    {
        Time.timeScale = 0.2f;
    }

    private void NormalTime()
    {
        Time.timeScale = 1;
    }

    private IEnumerator GoalSlowMo(GameStateManager gameStateManager)
    {
        SlowMo();
        float count = 2;

        while (count > 0)
        {
            count -= Time.deltaTime;
            foreach (GameObject player in gameStateManager.players)
            {
                Transform playerCam = player.transform.GetChild(0);
                Quaternion targetRotation = Quaternion.LookRotation(soccerBallBehavior.GetPosition() - playerCam.position);
                playerCam.rotation = Quaternion.Slerp(playerCam.rotation, targetRotation, Mathf.Log(3 * (2 - count) + 1));
            }
            yield return null;
        }

        foreach (GameObject player in gameStateManager.players)
        {
            Transform playerCam = player.transform.GetChild(0);
            playerCam.localEulerAngles = Vector3.zero;
        }

        NormalTime();
        gameStateManager.SwitchState(GetNextState(gameStateManager));
    }

    private GameStates GetNextState(GameStateManager gameStateManager)
    {
        if (gameStats.TimeIsUp() && !gameStats.ScoreTied())
        {
            return gameStateManager.gameOverState;
        }
        else
        {
            return gameStateManager.countdownState;
        }
    }
}
