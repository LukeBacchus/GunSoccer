using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.MENU;

    private GameObject pauseUI;
    private PauseManager pauseManager;

    public PauseState(GameObject pUI)
    {
        this.pauseUI = pUI;
        pauseManager = pauseUI.GetComponent<PauseManager>();
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        PauseGame();
        pauseUI.SetActive(true);
        pauseManager.SetUpSelector();
    }

    public override void UpdateState(GameStateManager gameStateManager) 
    {
        if (BackInput() && pauseManager.pauseStatus == PauseManager.PAUSESTATUS.None)
        {
            pauseUI.SetActive(false);
            ResumeGame();
            gameStateManager.SwitchState(gameStateManager.prevState);
        }

        pauseManager.PauseInput();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private bool BackInput()
    {
        return Input.GetButtonDown("Back1") | Input.GetButtonDown("Back2") | Input.GetButtonDown("Back3") | Input.GetButtonDown("Back4");
    }
}
