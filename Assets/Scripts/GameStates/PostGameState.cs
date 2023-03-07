using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostGameState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.POSTGAME;

    private GameStats gameStats;
    private GameObject announcement;

    public PostGameState(GameStats gameStats, GameObject announcement)
    {
        this.gameStats = gameStats;
        this.announcement = announcement;
    }

    public override void EnterState(GameStateManager gameStateManager)
    {
        gameStateManager.StartCoroutine(AfterGameAnnouncement(gameStateManager));
    }

    public override void UpdateState(GameStateManager gameStateManager)
    {

    }

    private IEnumerator AfterGameAnnouncement(GameStateManager gameStateManager)
    {
        yield return new WaitUntil(() => gameStats.TimeIsUp());

        GameStates nextState = gameStateManager.gameOverState;
        string text = "Time's Up!";
        if (gameStats.teamOneScore == gameStats.teamTwoScore)
        {
            text = "Overtime!";
            nextState = gameStateManager.countdownState;
        }
        else if (gameStateManager.prevState is GoalState)
        {
            text = "Game Set!";
            Debug.Log("ok");
        }

        announcement.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        announcement.GetComponent<Animator>().SetTrigger("broadcast_announcement");
        yield return new WaitForSeconds(2.5f);

        gameStateManager.SwitchState(nextState);
    }
}
