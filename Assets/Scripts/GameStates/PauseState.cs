using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseState : GameStates
{
    public override StateTypes stateType { get; } = StateTypes.MENU;

    //private List<bool> playersInSettings = new List<bool>();

    private MenuSelectionHelper menuSelector;

    private MenuSelectionHelper settingsSelector;

    private PauseStatus currentStatus;

    GameObject pauseMenuPanel;
    private Button settingsButton;
    private Button restartButton;
    private Button quitButton;

    GameObject settingsPanel;
    private Button backButton;
    private Button volumeUpButton;
    private Button volumeDownButton;

    int numPlayers;
    public enum PauseStatus
    {
        NONE,
        PAUSE,
        SETTINGS,
    }

    public PauseState(int numPlayers, GameObject pPanel, Button sButton,
        Button rButton, Button qButton, GameObject sPanel, Button bButton,
        Button vUpButton, Button vDownButton)
    {
        //for (int i = 0; i < numPlayers; i++)
        //{
        //    // since when UpdateState is called, player is already in settings
        //    // so we are always checking when to exit
        //    playersInSettings.Add(true);
        //}
        pauseMenuPanel = pPanel;
        settingsButton = sButton;
        restartButton = rButton;
        quitButton = qButton;

        settingsPanel = sPanel;
        backButton = bButton;
        volumeUpButton = vUpButton;
        volumeDownButton = vDownButton;


        settingsButton.onClick.AddListener(TransitionToSettings);
        restartButton.onClick.AddListener(TransitionToRestart);
        quitButton.onClick.AddListener(TransitionToQuit);

        // set the panels within this object to be inactive at the beginning
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);

        List<List<Button>> buttons = new List<List<Button>> { new List<Button> { settingsButton }, new List<Button> { quitButton } };
        menuSelector = new MenuSelectionHelper(buttons, 0, 1);

        SetupSettings(); // set up buttons and panel

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    }

    public override void EnterState(GameStateManager gameStateManager) 
    {
        TransitionToPause();
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
        if (currentStatus == PauseStatus.PAUSE)
        {
            if (Input.GetButtonDown("Menu"))
            {
                TransitionBackToGame();
                gameStateManager.SwitchState(gameStateManager.prevState);
            }
            else
            {
                PauseInput();
            }

        }
        else if (currentStatus == PauseStatus.SETTINGS)
        {
            SettingsInput();
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

    private void TransitionSettingsToPause()
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

    void PauseInput()
    {
        // deal with menu input
        menuSelector.SelectionInput();
        if (menuSelector.Select())
        {
            menuSelector.InvokeSelection();
        }
    }

    void SettingsInput()
    {
        settingsSelector.SelectionInput();
        if (settingsSelector.Select())
        {
            settingsSelector.InvokeSelection();
        }
    }

    private void SetupSettings()
    {
        backButton.onClick.AddListener(TransitionSettingsToPause);
        volumeUpButton.onClick.AddListener(SettingBehaviour.IncreaseVolume);
        volumeDownButton.onClick.AddListener(SettingBehaviour.DecreaseVolume);

        List<List<Button>> buttons = new List<List<Button>> { new List<Button> { volumeUpButton }, new List<Button> { volumeDownButton }, new List<Button> { backButton } };
        settingsSelector = new MenuSelectionHelper(buttons, 0, 3);

        settingsPanel.SetActive(false);
    }

}
