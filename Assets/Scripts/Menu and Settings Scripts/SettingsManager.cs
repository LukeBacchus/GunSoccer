using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenuPanel;

    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button volumeUpButton;
    [SerializeField]
    private Button volumeDownButton;

    [SerializeField]
    private GameObject oneVoneButtons;
    [SerializeField]
    private GameObject twoVtwoButtons;

    [SerializeField]
    GameObject settingsPanel;

    private GameStateManager manager;
    private PauseState state;
    private int playerNum;

    private MenuSelectionHelper settingsSelector;

    private float currentVolume;

    public void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
        state = manager.pauseState;
        playerNum = manager.players.Count;

        if (playerNum == 2)
        {
            twoVtwoButtons.SetActive(false);
        }
        else
        {
            oneVoneButtons.SetActive(false);
        }
        SetupSettings();
    }


    void SetupSettings()
    {
        backButton.onClick.AddListener(TransitionSettingsToPause);
        volumeUpButton.onClick.AddListener(IncreaseVolume);
        volumeDownButton.onClick.AddListener(DecreaseVolume);

        List<List<Button>> buttons = new List<List<Button>> { new List<Button> { volumeUpButton }, new List<Button> { volumeDownButton }, new List<Button> { backButton } };
        settingsSelector = new MenuSelectionHelper(buttons, 0, 2);

        currentVolume = AudioListener.volume;
    }


    public void SettingsInput()
    {
        settingsSelector.SelectionInput();
        if (settingsSelector.Select())
        {
            settingsSelector.InvokeSelection();
        }
    }
    
    private void TransitionSettingsToPause()
    {
        pauseMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        state.SetPause();
    }

    void IncreaseSensitivity()
    {
        // int playerNum = 1;
        Debug.Log("not implemented yet");
    }

    void DecreaseSensivity()
    {
        // int playerNum = 1;
        Debug.Log("not implemented yet");
    }

    void IncreaseVolume()
    {
        Debug.Log("increase volume not implemented yet");
        float newVolume = currentVolume;
        if (currentVolume < 1)
        {
            // if can increase volume
            newVolume = currentVolume + 0.1f;
        }
        SetVolume(newVolume);
    }

    void DecreaseVolume()
    {
        Debug.Log("decrease volume not implemented yet");

        float newVolume = currentVolume;
        if (currentVolume > 0)
        {
            // if can decrease volume
            newVolume = currentVolume + 0.1f;
        }
        SetVolume(newVolume);
    }

    void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        currentVolume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
