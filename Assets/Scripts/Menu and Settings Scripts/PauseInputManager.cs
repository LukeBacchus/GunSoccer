using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseInputManager : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenuPanel;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button controlsButton;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    GameObject settingsPanel;

    private MenuSelectionHelper menuSelector;
    PauseState state;

    // awake is called before start, so makes sure that it's stuff is set up??
    public void Awake()
    {
        settingsButton.onClick.AddListener(TransitionToSettings);
        controlsButton.onClick.AddListener(showControlsInstruction);
        resumeButton.onClick.AddListener(TransitionBackToGame);
        quitButton.onClick.AddListener(TransitionToQuit);

        List<List<Button>> pauseButtons = new List<List<Button>> { new List<Button> { settingsButton }, new List<Button> { controlsButton }, new List<Button> { resumeButton }, new List<Button> { quitButton } };
        this.menuSelector = new MenuSelectionHelper(pauseButtons, 0, 3, new List<int> { 1, 2, 3, 4 });

    }

    public void Start()
    {
        GameStateManager manager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
        state = manager.pauseState;
    }

    public void PauseInput()
    {
        // deal with menu input
        menuSelector.SelectionInput();
        if (menuSelector.Select())
        {
            menuSelector.InvokeSelection();
        }
    }

    private void TransitionToSettings()
    {
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        GameStateManager gm = GameObject.Find("GameManager").GetComponent<GameStateManager>();
        PauseState ps = (PauseState)gm.currentState;
        ps.SetSettings();
    }

    private void TransitionToQuit()
    {
        SceneManager.LoadScene("Start Screen");
    }

    private void showControlsInstruction()
    {
        Debug.Log("can't show controls yet");
    }

    public void TransitionSettingsToPause()
    {
        settingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
        state.SetPause();
    }


    private void TransitionBackToGame()
    {
        pauseMenuPanel.SetActive(false);
        state.SetExit();
    }

}
