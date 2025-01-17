using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private SettingsManager settingsManager;

    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private GameObject controlsPanel;

    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button controlsButton;
    [SerializeField]
    private Button quitButton;

    private MenuSelectionHelper menuSelector;

    public PAUSESTATUS pauseStatus = PAUSESTATUS.None;

    public enum PAUSESTATUS
    {
        None,
        Controls,
        Settings
    }

    // Start is called before the first frame update
    void Awake()
    {
        settingsButton.onClick.AddListener(TransitionToSettings);
        controlsButton.onClick.AddListener(TransitionToControls);
        quitButton.onClick.AddListener(TransitionToQuit);
    }

    public void SetUpSelector()
    {
        List<List<GameObject>> pauseButtons = new List<List<GameObject>> { new List<GameObject> { settingsButton.gameObject },
            new List<GameObject> { controlsButton.gameObject }, new List<GameObject> { quitButton.gameObject } };
        menuSelector = new MenuSelectionHelper(pauseButtons, 0, 2, new List<int> { 1, 2, 3, 4 });
        settingsManager.SetupSelector();
    }

    public void PauseInput()
    {
        if (BackInput())
        {
            if (pauseStatus == PAUSESTATUS.Settings)
            {
                settingsManager.ResetSelector();
                pauseStatus = PAUSESTATUS.None;
                settingsPanel.SetActive(false);
            }
            else if (pauseStatus == PAUSESTATUS.Controls)
            {
                pauseStatus = PAUSESTATUS.None;
                pauseStatus = PAUSESTATUS.None;
                controlsPanel.SetActive(false);
            }
        }

        if (pauseStatus == PAUSESTATUS.None)
        {
            menuSelector.SelectionInput();
            if (menuSelector.Select())
            {
                menuSelector.InvokeSelection();
            }
        }
        else if (pauseStatus == PAUSESTATUS.Settings)
        {
            settingsManager.SettingsInput();
        }
    }

    private void TransitionToSettings()
    {
        settingsPanel.SetActive(true);
        pauseStatus = PAUSESTATUS.Settings;
    }

    private void TransitionToQuit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Screen");
    }

    private void TransitionToControls()
    {
        controlsPanel.SetActive(true);
        pauseStatus = PAUSESTATUS.Controls;
    }

    private bool BackInput()
    {
        return Input.GetButtonDown("Back1") | Input.GetButtonDown("Back2") | Input.GetButtonDown("Back3") | Input.GetButtonDown("Back4");
    }
}