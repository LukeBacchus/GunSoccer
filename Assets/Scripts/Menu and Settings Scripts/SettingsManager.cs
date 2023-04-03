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
    private GameObject volumeSetting;

    [SerializeField]
    private GameObject player1sens;
    [SerializeField]
    private GameObject player1angle;
    [SerializeField]
    private GameObject player1strength;

    [SerializeField]
    private GameObject player2sens;
    [SerializeField]
    private GameObject player2angle;
    [SerializeField]
    private GameObject player2strength;

    [SerializeField]
    private GameObject player3sens;
    [SerializeField]
    private GameObject player3angle;
    [SerializeField]
    private GameObject player3strength;

    [SerializeField]
    private GameObject player4sens;
    [SerializeField]
    private GameObject player4angle;
    [SerializeField]
    private GameObject player4strength;

    [SerializeField]
    GameObject settingsPanel;

    private MenuSelectionHelper settingsSelector;


    public void Start()
    {

    }

    public void SettingsInput()
    {
        settingsSelector.SelectionInput();
        if (settingsSelector.Select())
        {
            settingsSelector.InvokeSelection();
        }
    }
}
