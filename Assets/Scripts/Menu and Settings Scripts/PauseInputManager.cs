using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void Start()
    {
        GameStateManager manager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
        state = manager.pauseState;
        settingsButton.onClick.AddListener(TransitionToSettings);
        controlsButton.onClick.AddListener(showControlsInstruction);
        resumeButton.onClick.AddListener(TransitionBackToGame);
        quitButton.onClick.AddListener(TransitionToQuit);

        List<List<Button>> buttons = new List<List<Button>> { new List<Button> { settingsButton }, new List<Button> { controlsButton }, new List<Button> { resumeButton }, new List<Button> { quitButton } };
        menuSelector = new MenuSelectionHelper(buttons, 0, 3, new List<int> { 1, 2, 3, 4 });

        // does this when first active, letting itself be shown rather than settings
        pauseMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
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

    }

    private void TransitionToQuit()
    {
        Debug.Log("quiting not implemented yet");
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
