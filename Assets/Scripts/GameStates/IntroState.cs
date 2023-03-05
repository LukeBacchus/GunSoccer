using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.CINEMATIC;

    private GameObject fourPlayerUI;
    private GameObject twoPlayerUI;
    private GameObject scoreBoard;

    public IntroState(GameObject twoPlayerUI, GameObject fourPlayerUI, GameObject scoreBoard)
    {
        this.fourPlayerUI = fourPlayerUI;
        this.twoPlayerUI = twoPlayerUI;
        this.scoreBoard = scoreBoard;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        Debug.Log("IntroState not implemented yet.");

        if (gameStateManager.players.Count == 2)
        {
            twoPlayerUI.SetActive(true);
            fourPlayerUI.SetActive(false);
        }
        else
        {
            twoPlayerUI.SetActive(false);
            fourPlayerUI.SetActive(true);
        }

        scoreBoard.SetActive(true);

        gameStateManager.SwitchState(gameStateManager.countdownState);
    }
    public override void UpdateState(GameStateManager gameStateManager) { }
}
