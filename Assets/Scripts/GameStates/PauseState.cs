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

    GameObject pUI;
    GameObject pauseMenuPanel;
    private Button settingsButton;
    private Button restartButton;
    private Button resumeButton;
    private Button quitButton;

    GameObject settingsPanel;
    SettingsManager settingsManager;


    int numPlayers;
    public enum PauseStatus
    {
        NONE,
        PAUSE,
        SETTINGS,
    }
    bool canExit;

    public PauseState(int numPlayers,GameObject pauseUI, GameObject pPanel, Button sButton,
        Button restButton,Button resuButton, Button qButton, GameObject sPanel, Button bButton,
        Button vUpButton, Button vDownButton)
    {
        //for (int i = 0; i < numPlayers; i++)
        //{
        //    // since when UpdateState is called, player is already in settings
        //    // so we are always checking when to exit
        //    playersInSettings.Add(true);
        //}
        pUI = pauseUI;
        pauseMenuPanel = pPanel;
        settingsButton = sButton;
        restartButton = restButton;
        resumeButton = resuButton;
        quitButton = qButton;

        settingsPanel = sPanel;
        settingsPanel.SetActive(false);
        settingsManager = GameObject.Find("Settings Panel").GetComponent<SettingsManager>();

        SetupPause();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        pUI.SetActive(true);
        TransitionToPause();
        canExit = false;
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
            TransitionBackToGame();
            gameStateManager.SwitchState(gameStateManager.prevState);
        }

        if (currentStatus == PauseStatus.PAUSE)
        {
            PauseInput();
        }
        else if (currentStatus == PauseStatus.SETTINGS)
        {
            settingsManager.SettingsInput();
            //SettingsInput();
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

    private void TransitionToSettings()
    {
        pauseMenuPanel.SetActive(false);
        currentStatus = PauseStatus.SETTINGS;
        settingsPanel.SetActive(true);
    }

    private void TransitionToQuit()
    {
        Debug.Log("quiting not implemented yet");
    }

    private void TransitionToRestart()
    {
        Debug.Log("restarting not implemented yet");
    }

    public static void TransitionSettingsToPause()
    {
        currentStatus = PauseStatus.PAUSE;
        settingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    private void TransitionBackToGame()
    {
        currentStatus = PauseStatus.NONE;
        //if the menu is not active yet, set it to be active
        pauseMenuPanel.SetActive(false);
        //pause the game
        ResumeGame();
    }

    private void TransitionToPause()
    {
        currentStatus = PauseStatus.PAUSE;
        //if the menu is not active yet, set it to be active
        pauseMenuPanel.SetActive(true);
        //pause the game
        PauseGame();
    }

    private void SetExit()
    {
        canExit = true;
    }

    void PauseInput()
    {
        // deal with menu input
        menuSelector.SelectionInput();
        if (menuSelector.Select())
        {
            menuSelector.InvokeSelection();
        }
    }


    private void SetupPause()
    {
        settingsButton.onClick.AddListener(TransitionToSettings);
        restartButton.onClick.AddListener(TransitionToRestart);
        resumeButton.onClick.AddListener(SetExit);
        quitButton.onClick.AddListener(TransitionToQuit);

        // set the panels within this object to be inactive at the beginning
        pauseMenuPanel.SetActive(false);

        List<List<Button>> buttons = new List<List<Button>> { new List<Button> { settingsButton }, new List<Button> { restartButton }, new List<Button> { resumeButton },new List<Button> { quitButton } };
        menuSelector = new MenuSelectionHelper(buttons, 0, 3, new List<int> { 1, 2, 3, 4 });
    }



}
