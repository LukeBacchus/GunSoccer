using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject settingsPanel;
    // Start is called before the first frame update
    void Awake()
    {
        
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

    public void PauseInput()
    {
        PauseInputManager pauseInputManager =pausePanel.GetComponent<PauseInputManager>();
        pauseInputManager.PauseInput();
    }

    public void SettingsInput()
    {
        SettingsManager settingsManager = settingsPanel.GetComponent<SettingsManager>();
        settingsManager.SettingsInput();
    }
}