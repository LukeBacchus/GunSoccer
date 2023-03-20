using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.MENU;

    //private List<bool> playersInSettings = new List<bool>();

    private MenuSelectionHelper menuSelector;

    private PauseStatus currentStatus;
    private PauseStatus prevStatus;

    private GameObject pauseUI;

    public enum PauseStatus
    {
        NONE,
        PAUSE,
        SETTINGS,
    }
    bool canExit;


    public PauseState(GameObject pUI)
    {
        //for (int i = 0; i < numPlayers; i++)
        //{
        //    // since when UpdateState is called, player is already in settings
        //    // so we are always checking when to exit
        //    playersInSettings.Add(true);
        //}

        this.pauseUI = pUI;

        //GameObject settingsPanel = GameObject.Find("Settings Panel").gameObject;
        //GameObject pausePanel = GameObject.Find("Pause Panel").gameObject;

        //settingsPanel.SetActive(false);
        //pausePanel.SetActive(false);

        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        PauseGame();
        pauseUI.SetActive(true);
        canExit = false;
        currentStatus = PauseStatus.PAUSE;
    }

    public override void UpdateState(GameStateManager gameStateManager) 
    {
        //below code deal with multiplayer different exit

        //bool canExit = false;
        //updatePlayerInput(); - not implemented yet
        //for (int i = 0; i < playersInSettings.Count; i++)
        //{
        //    canExit |= !playersInSettings[i];
        //}

        //if (canExit)
        //{
        //    TransitionBackToGame();
        //    gameStateManager.SwitchState(gameStateManager.prevState);
        //}

        //(currently using one single "Menu" mapping in inputManager,
        //thus code above temporarily not needed)

        if (canExit)
        {
            ResumeGame();
            //pauseUI.SetActive(false);
            gameStateManager.SwitchState(gameStateManager.prevState);
        }

        if (currentStatus == PauseStatus.PAUSE)
        {
            PauseInputManager pauseManager = GameObject.Find("PausePanel").GetComponent<PauseInputManager>();
            pauseManager.PauseInput();
        }
        else if (currentStatus == PauseStatus.SETTINGS)
        {
            SettingsManager settingsManager = GameObject.Find("SettingsPanel").GetComponent<SettingsManager>();
            settingsManager.SettingsInput();
        }

    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void SetExit()
    {
        canExit = true;
    }

    public void SetPause()
    {
        currentStatus = PauseStatus.PAUSE;
    }

    public void SetSettings()
    {
        currentStatus = PauseStatus.SETTINGS;
    }


}