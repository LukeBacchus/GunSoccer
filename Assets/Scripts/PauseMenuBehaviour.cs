using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenuPanel;

    private GameStats gameStats;
    private MenuSelectionHelper menuSelector;

    [SerializeField]
    GameObject settingsPanel;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button quitButton;
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
        // set the panel within this object to be inactive
        pauseMenuPanel.SetActive(false);
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();

        List<List<Button>> buttons = new List<List<Button>> { new List<Button> { settingsButton }, new List<Button> { quitButton }};
        menuSelector = new MenuSelectionHelper(buttons, 0, 1);


    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("Menu"))
        {
            Debug.Log("Menu pressed");
            if (!pauseMenuPanel.activeSelf)
            {
                currentPanel = PauseStatus.PAUSE;
                //if the menu is not active yet, set it to be active
                pauseMenuPanel.SetActive(true);
                //TODO: pause the game
                gameStats.PauseGame();
            }
            else
            {
                currentPanel = PauseStatus.NONE;
                pauseMenuPanel.SetActive(false);
                gameStats.ResumeGame();
            }
            
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
        Debug.Log("settings input not dealt with yet"); 
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
