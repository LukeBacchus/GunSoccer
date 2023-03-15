using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button volumeUpButton;
    [SerializeField]
    private Button volumeDownButton;

    [SerializeField]
    private Button sensUpButton;
    [SerializeField]
    private Button sensDownButton;

    //[SerializeField]
    //private GameObject oneVoneButtons;
    //[SerializeField]
    //private GameObject twoVtwoButtons;

    private GameStateManager manager;
    private int player_num;

    private MenuSelectionHelper settingsSelector;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
        player_num = manager.players.Count;
        //if (player_num == 2)
        //{
        //    twoVtwoButtons.SetActive(false);
        //}
        //else
        //{
        //    oneVoneButtons.SetActive(false);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupSettings()
    {
        backButton.onClick.AddListener(PauseState.TransitionSettingsToPause);
        volumeUpButton.onClick.AddListener(SettingBehaviour.IncreaseVolume);
        volumeDownButton.onClick.AddListener(SettingBehaviour.DecreaseVolume);

        sensUpButton.onClick.AddListener(SettingBehaviour.IncreaseSensitivity(1));
        sensDownButton.onClick.AddListener(SettingBehaviour.DecreaseSensivity(1));
        List<List<Button>> buttons = new List<List<Button>> { new List<Button> { volumeUpButton }, new List<Button> { volumeDownButton }, new List<Button> { sensUpButton }, new List<Button> { sensDownButton }, new List<Button> { backButton } };
        settingsSelector = new MenuSelectionHelper(buttons, 0, 2);
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
