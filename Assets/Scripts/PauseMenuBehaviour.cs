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
        settingsPanel.SetActive(false);

        
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();

        List<List<Button>> buttons = new List<List<Button>> { new List<Button> { settingsButton }, new List<Button> { quitButton }};
        menuSelector = new MenuSelectionHelper(buttons, 0, 1);

        // TODO: this is not updated with correct buttons: change 
        SetupSettings();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Menu"))
        {
            Debug.Log("Menu pressed");
            if (currentPanel == PauseStatus.NONE)
            {
                TransitionToPause();
            }
            else if (currentPanel == PauseStatus.PAUSE)
            {
                TransitionBackToGame();
            }
            // if menu button pressed when in menu, can't immediately unpause
        }

        if(currentPanel == PauseStatus.PAUSE)
        {
            PauseInput();
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
        
        List<List<Button>> buttons = new List<List<Button>> { new List<Button> {volumeUpButton, volumeDownButton}, new List<Button> { quitButton} };

        settingsSelector = new MenuSelectionHelper(buttons, 0, 3);

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
        //TODO: add the 
    }

    void TransitionBackToGame()
    {
        currentPanel = PauseStatus.NONE;
        //if the menu is not active yet, set it to be active
        pauseMenuPanel.SetActive(false);
        //pause the game
        gameStats.ResumeGame();
    }

    void TransitionToPause()
    {
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
}
