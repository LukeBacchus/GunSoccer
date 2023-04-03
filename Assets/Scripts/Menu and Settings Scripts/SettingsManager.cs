using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private MenuSliderController volumeSetting;

    [SerializeField]
    private MenuSliderController player1sens;
    [SerializeField]
    private MenuSliderController player1angle;
    [SerializeField]
    private MenuSliderController player1strength;

    [SerializeField]
    private MenuSliderController player2sens;
    [SerializeField]
    private MenuSliderController player2angle;
    [SerializeField]
    private MenuSliderController player2strength;

    [SerializeField]
    private MenuSliderController player3sens;
    [SerializeField]
    private MenuSliderController player3angle;
    [SerializeField]
    private MenuSliderController player3strength;

    [SerializeField]
    private MenuSliderController player4sens;
    [SerializeField]
    private MenuSliderController player4angle;
    [SerializeField]
    private MenuSliderController player4strength;

    private MenuSelectionHelper settingsSelector;
    private List<List<GameObject>> menuOptions;


    public void Start()
    {
        menuOptions = new List<List<GameObject>> { new List<GameObject>{ volumeSetting.gameObject },
            new List<GameObject>{ player1sens.gameObject }, new List<GameObject>{ player1angle.gameObject }, new List<GameObject>{ player1strength.gameObject },
            new List<GameObject>{ player2sens.gameObject }, new List<GameObject> { player2angle.gameObject }, new List<GameObject> { player2strength.gameObject },
                new List<GameObject>{player3sens.gameObject }, new List<GameObject> { player3angle.gameObject }, new List<GameObject> { player3strength.gameObject } };
        settingsSelector = new MenuSelectionHelper(menuOptions, 0, 12, new List<int>{ 1, 2, 3, 4 });
    }

    public void SettingsInput()
    {
        settingsSelector.SelectionInput();
        settingsSelector.GetCurrent().GetComponent<MenuSliderController>().SliderInput();
        settingsSelector.GetCurrent().GetComponent<MenuSliderController>().updateText();
        UpdateSettingsConstants();
    }

    private void UpdateSettingsConstants()
    {
        GameSettings.currVolume = volumeSetting.getValue();
        GameSettings.Player1Settings = new List<float> { player1sens.getValue(), player1angle.getValue(), player1strength.getValue() };
        GameSettings.Player2Settings = new List<float> { player2sens.getValue(), player2angle.getValue(), player2strength.getValue() };
        GameSettings.Player3Settings = new List<float> { player3sens.getValue(), player3angle.getValue(), player3strength.getValue() };
        GameSettings.Player4Settings = new List<float> { player4sens.getValue(), player4angle.getValue(), player4strength.getValue() };
    }
    
}
