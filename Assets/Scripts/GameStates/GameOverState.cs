using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.MENU;

    private GameObject winUI;

    public GameOverState(GameObject winUI)
    {
        this.winUI = winUI;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        winUI.SetActive(true);
        winUI.GetComponent<WinStats>().DisplayWinner();
    }

    public override void UpdateState(GameStateManager gameStateManager) { }
}
