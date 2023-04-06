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
        soccerBallBehavior.Explode();
        gameStateManager.StartCoroutine(GoalSlowMo(gameStateManager));
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {
        if (Input.GetButtonDown("Menu"))
        {
            gameStateManager.SwitchState(gameStateManager.pauseState);
        }
    }

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
        float count = 1;

        bool[] arrowActives = new bool[gameStateManager.arrows.Count];
        int i = 0;

        foreach (GameObject arrow in gameStateManager.arrows)
        {
            arrowActives[i] = arrow.active;
            arrow.SetActive(false);
            arrow.transform.parent.gameObject.GetComponent<ArrowRotator>().visible = false;
            i++;
        }

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

        i = 0;
        foreach (GameObject arrow in gameStateManager.arrows)
        {
            arrow.SetActive(arrowActives[i]);
            arrow.transform.parent.gameObject.GetComponent<ArrowRotator>().visible = true;
            i++;
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