using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinStats: MonoBehaviour 
{
    public float timer;
    [SerializeField]
    private Button replayButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button creditsButton;
    private MenuSelectionHelper winSelector;

    public void DisplayWinner () 
    {
        replayButton.onClick.AddListener(SelectedReplay);
        quitButton.onClick.AddListener(SelectedQuit);
        creditsButton.onClick.AddListener(SelectedCredits);

        List<List<GameObject>> winButtons = new List<List<GameObject>> { new List<GameObject> { replayButton.gameObject }, 
            new List<GameObject> { quitButton.gameObject }, new List<GameObject> { creditsButton.gameObject } };
        winSelector = new MenuSelectionHelper(winButtons, 0, 2, new List<int> { 1, 2, 3, 4 });
    }
    public void UpdateWinScreen()
    {
        winSelector.SelectionInput();
        if (winSelector.Select())
        {
            winSelector.InvokeSelection();
        }
    }

    private void SelectedReplay()
    {
        SceneManager.LoadScene("Start Screen");
    }

    private void SelectedQuit()
    {
        Application.Quit();
    }

    private void SelectedCredits()
    {
        Debug.Log("Selected credits. Credits Not Implemented yet.");
    }

}
