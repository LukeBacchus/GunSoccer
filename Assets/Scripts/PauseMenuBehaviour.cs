using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenuPanel;

    private GameStats gameStats;

    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button quitButton;

    private MenuSelectionHelper menuSelector;

    [SerializeField]
    GameObject settingsPanel;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button volumeUpButton;
    [SerializeField]
    private Button volumeDownButton;

    private MenuSelectionHelper settingsSelector;

    private PauseStatus currentPanel;

    int num_players;
    public enum PauseStatus
    {
        NONE,
        PAUSE,
        SETTINGS,
    }
    // Start is called before the first frame update
    void Start()
    {
        settingsButton.onClick.AddListener(TransitionToSettings);
        quitButton.onClick.AddListener(TransitionToQuit);

        // set the panel within this object to be inactive at the beginning
        pauseMenuPanel.SetActive(false);
        
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();

        List<List<Button>> buttons = new List<List<Button>> { new List<Button> { settingsButton }, new List<Button> { quitButton }};
        menuSelector = new MenuSelectionHelper(buttons, 0, 1);

        SetupSettings(); // set up buttons and panel

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        num_players = players.Length; 
        // save the length of players for easier toggling of UI later

    }

    // Update is called once per frame
    void Update()
    {
        if(currentPanel == PauseStatus.NONE)
        {
            if (Input.GetButtonDown("Menu"))
            {
                TransitionToPause();
            }
        }
        else if(currentPanel == PauseStatus.PAUSE)
        {
            if (Input.GetButtonDown("Menu"))
            {
                TransitionBackToGame();
            }
            else
            {
                PauseInput();
            }
            
        }
        else if (currentPanel == PauseStatus.SETTINGS)
        {
            SettingsInput();
        }
    }

    void SetupSettings()
    {
        backButton.onClick.AddListener(TransitionSettingsToPause);
        volumeUpButton.onClick.AddListener(SettingBehaviour.IncreaseVolume);
        volumeDownButton.onClick.AddListener(SettingBehaviour.DecreaseVolume);
        
        List<List<Button>> buttons = new List<List<Button>> { new List<Button> {volumeUpButton}, new List<Button> { volumeDownButton}, new List<Button> {backButton} };
        settingsSelector = new MenuSelectionHelper(buttons, 0, 3);

        settingsPanel.SetActive(false);
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

    void TransitionSettingsToPause()
    {
        currentPanel = PauseStatus.PAUSE;
        settingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    void TransitionBackToGame()
    {
        //showUI();
        currentPanel = PauseStatus.NONE;
        //if the menu is not active yet, set it to be active
        pauseMenuPanel.SetActive(false);
        //pause the game
        gameStats.ResumeGame();
    }

    void TransitionToPause()
    {
        //hideUI();
        currentPanel = PauseStatus.PAUSE;
        //if the menu is not active yet, set it to be active
        pauseMenuPanel.SetActive(true);
        //pause the game
        gameStats.PauseGame();
    }
    void TransitionToSettings()
    {
        pauseMenuPanel.SetActive(false);
        currentPanel = PauseStatus.SETTINGS;
        settingsPanel.SetActive(true);
    }

    void TransitionToQuit()
    {
        Debug.Log("quiting not implemented yet");
    }

    void hideUI()
    {   
        // currently hiding and showing UI objects by SetActive()
        // which might not be the best choice
        if (num_players == 2)
        {
            GameObject two_playerUI = GameObject.Find("1v1 UI");
            two_playerUI.SetActive(false);
        }
        else
        {
            GameObject four_playerUI = GameObject.Find("2v2 UI");
            four_playerUI.SetActive(false);
        }

        GameObject scoreboard = GameObject.Find("ScoreBoard");
        scoreboard.SetActive(false);

    }

    void showUI()
    {
        if (num_players == 2)
        {
            GameObject two_playerUI = GameObject.Find("1v1 UI");
            two_playerUI.SetActive(true);
        }
        else
        {
            GameObject four_playerUI = GameObject.Find("2v2 UI");
            four_playerUI.SetActive(true);
        }

        GameObject scoreboard = GameObject.Find("ScoreBoard");
        scoreboard.SetActive(true);

    }
}
