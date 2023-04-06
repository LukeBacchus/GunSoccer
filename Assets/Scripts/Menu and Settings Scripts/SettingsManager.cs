using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject volumeSetting;
    [SerializeField]
    private MenuSliderController volumeSettingSlider;

    [SerializeField]
    private GameObject player1sens;
    [SerializeField]
    private MenuSliderController player1sensSlider;
    [SerializeField]
    private GameObject player1angle;
    [SerializeField]
    private MenuSliderController player1angleSlider;
    [SerializeField]
    private GameObject player1strength;
    [SerializeField]
    private MenuSliderController player1strengthSlider;

    [SerializeField]
    private GameObject player2sens;
    [SerializeField]
    private MenuSliderController player2sensSlider;
    [SerializeField]
    private GameObject player2angle;
    [SerializeField]
    private MenuSliderController player2angleSlider;
    [SerializeField]
    private GameObject player2strength;
    [SerializeField]
    private MenuSliderController player2strengthSlider;

    [SerializeField]
    private GameObject player3sens;
    [SerializeField]
    private MenuSliderController player3sensSlider;
    [SerializeField]
    private GameObject player3angle;
    [SerializeField]
    private MenuSliderController player3angleSlider;
    [SerializeField]
    private GameObject player3strength;
    [SerializeField]
    private MenuSliderController player3strengthSlider;

    [SerializeField]
    private GameObject player4sens;
    [SerializeField]
    private MenuSliderController player4sensSlider;
    [SerializeField]
    private GameObject player4angle;
    [SerializeField]
    private MenuSliderController player4angleSlider;
    [SerializeField]
    private GameObject player4strength;
    [SerializeField]
    private MenuSliderController player4strengthSlider;

    [SerializeField]
    private RectTransform viewport;
    [SerializeField]
    private RectTransform grid;

    private MenuSelectionHelper settingsSelector;
    private List<List<GameObject>> menuOptions;

    private void Start()
    {
        ResetGridPosition();
        InitializeSliders();
    }

    public void SetupSelector()
    {
        menuOptions = new List<List<GameObject>> { new List<GameObject>{ volumeSetting },
            new List<GameObject>{ player1sens }, new List<GameObject>{ player1angle }, new List<GameObject>{ player1strength },
            new List<GameObject>{ player2sens }, new List<GameObject> { player2angle }, new List<GameObject> { player2strength },
            new List<GameObject>{ player3sens }, new List<GameObject> { player3angle }, new List<GameObject> { player3strength },
            new List<GameObject>{ player4sens }, new List<GameObject> { player4angle }, new List<GameObject> { player4strength }};
        settingsSelector = new MenuSelectionHelper(menuOptions, 0, 12, viewport, grid, false, true, new List<int> { 1, 2, 3, 4 });
    }

    public void ResetGridPosition()
    {
        grid.position = new Vector3(grid.position.x, 0, grid.position.z);
    }

    public void ResetSelector()
    {
        settingsSelector.ResetCurrent();
        ResetGridPosition();
    }

    public void SettingsInput()
    {
        settingsSelector.SelectionInput();
        settingsSelector.GetCurrent().GetComponentInChildren<MenuSliderController>().SliderInput();
        settingsSelector.GetCurrent().GetComponentInChildren<MenuSliderController>().updateText();
        UpdateSettingsConstants();
    }

    public void InitializeSliders()
    {
        volumeSettingSlider.InitValue(GameSettings.currVolume);

        player1sensSlider.InitValue(GameSettings.Player1Settings[0]);
        player1angleSlider.InitValue(GameSettings.Player1Settings[1]);
        player1strengthSlider.InitValue(GameSettings.Player1Settings[2]);

        player2sensSlider.InitValue(GameSettings.Player2Settings[0]);
        player2angleSlider.InitValue(GameSettings.Player2Settings[1]);
        player2strengthSlider.InitValue(GameSettings.Player2Settings[2]);

        player3sensSlider.InitValue(GameSettings.Player3Settings[0]);
        player3angleSlider.InitValue(GameSettings.Player3Settings[1]);
        player3strengthSlider.InitValue(GameSettings.Player3Settings[2]);

        player4sensSlider.InitValue(GameSettings.Player4Settings[0]);
        player4angleSlider.InitValue(GameSettings.Player4Settings[1]);
        player4strengthSlider.InitValue(GameSettings.Player4Settings[2]);
    }

    private void UpdateSettingsConstants()
    {
        GameSettings.currVolume = volumeSettingSlider.getValue();
        GameSettings.Player1Settings = new List<float> { 
            player1sensSlider.getValue(), 
            player1angleSlider.getValue(), 
            player1strengthSlider.getValue() };
        GameSettings.Player2Settings = new List<float> { 
            player2sensSlider.getValue(), 
            player2angleSlider.getValue(), 
            player2strengthSlider.getValue() };
        GameSettings.Player3Settings = new List<float> { 
            player3sensSlider.getValue(), 
            player3angleSlider.getValue(), 
            player3strengthSlider.getValue() };
        GameSettings.Player4Settings = new List<float> { 
            player4sensSlider.getValue(), 
            player4angleSlider.getValue(), 
            player4strengthSlider.getValue() };
    }

 
}
