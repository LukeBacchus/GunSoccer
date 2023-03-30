using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button controlsButton;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button quitButton;

    private MenuSelectionHelper menuSelector;
    PauseState state;

    // Start is called before the first frame update
    void Awake()
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

    // Update is called once per frame
    public void SetSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void SetPause()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void SettingsInput()
    {
        SettingsManager settingsManager = settingsPanel.GetComponent<SettingsManager>();
        settingsManager.SettingsInput();
    }

    public void PauseInput()
    {
        //this is used to debug with keyboard DELETE if not needed
        if (Input.GetButtonDown("Menu"))
        {
            TransitionBackToGame();
        }
        // deal with menu input
        menuSelector.SelectionInput();
        if (menuSelector.Select())
        {
            menuSelector.InvokeSelection();
        }
    }

    private void TransitionToSettings()
    {
        pausePanel.SetActive(false);
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
        pausePanel.SetActive(true);
        state.SetPause();
    }


    private void TransitionBackToGame()
    {
        pausePanel.SetActive(false);
        state.SetExit();
    }
}