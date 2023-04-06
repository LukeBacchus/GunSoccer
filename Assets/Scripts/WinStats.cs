
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
    [SerializeField]
    private GameObject creditsPanel;

    private MenuSelectionHelper winSelector;
    private bool openedCredits = false;

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
        if (openedCredits)
        {
            CreditsInput();
        }
        else
        {
            winSelector.SelectionInput();
            if (winSelector.Select())
            {
                winSelector.InvokeSelection();
            }
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
        creditsPanel.SetActive(true);
        openedCredits = true;
    }

    private void CreditsInput()
    {
        if (Input.GetButtonDown("Back1") | Input.GetButtonDown("Back2") | Input.GetButtonDown("Back3") | Input.GetButtonDown("Back4"))
        {
            creditsPanel.SetActive(false);
            openedCredits = false;
        }
    }

}
