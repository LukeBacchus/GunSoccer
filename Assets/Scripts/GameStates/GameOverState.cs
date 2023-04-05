using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.MENU;

    private GameObject winUI;
    private GameObject statsUI;
    private string display = "stats";

    public GameOverState(GameObject winUI, GameObject statsUI)
    {
        this.winUI = winUI;
        this.statsUI = statsUI;
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        statsUI.SetActive(true);
        statsUI.GetComponent<EndStats>().DisplayStats();
    }

    public override void UpdateState(GameStateManager gameStateManager) {
        if (Input.GetButtonDown("Menu"))
        {
            gameStateManager.SwitchState(gameStateManager.pauseState);
        }

        if (display == "stats") {
            if (Input.GetButtonDown("Jump1") || Input.GetButtonDown("Jump2") || Input.GetButtonDown("Jump3") || Input.GetButtonDown("Jump4"))
            {
                statsUI.SetActive(false);
                winUI.SetActive(true);
                winUI.GetComponent<WinStats>().DisplayWinner();
                display = "win";
            } 
        }
        else {
            winUI.GetComponent<WinStats>().UpdateWinScreen();
        }
    }
}
