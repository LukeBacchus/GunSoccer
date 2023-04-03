using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinStats: MonoBehaviour 
{
    private TMPro.TextMeshProUGUI winnerText;
    public float timer;
    private GameStats gameStats;
    private string winner;
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

        // gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();
        // if (gameStats.teamOneScore > gameStats.teamTwoScore) {
        //     winner = "Team 1";
        // }
        // else if (gameStats.teamTwoScore > gameStats.teamOneScore) {
        //     winner = "Team 2";
        // }
        // else {
        //     winner = "No One";
        // }
        // winnerText = GameObject.Find("Win Text").GetComponent<TMPro.TextMeshProUGUI>();
        // timer = 15.99f;
        // winnerText.text = winner + " Won!!! Congrats!!! Returning To Main Menu In: 15";

        // StartCoroutine(UpdateWinScreen());
    }
    public void UpdateWinScreen()
    {
        winSelector.SelectionInput();
        if (winSelector.Select())
        {
            winSelector.InvokeSelection();
        }
        // while (timer > 0)
        // {
        //     timer -= Time.deltaTime;
        //     winnerText.text = winner + " Won!!! Congrats!!! Returning To Main Menu In: " + (int) timer;
        //     yield return null;
        // }

        // SceneManager.LoadScene("Start Screen"); 
    }

    private void SelectedReplay()
    {
        Debug.Log("Selected replay. Replay Not Implemented yet.");
    }

    private void SelectedQuit()
    {
        SceneManager.LoadScene("Start Screen"); 
    }

    private void SelectedCredits()
    {
        Debug.Log("Selected credits. Credits Not Implemented yet.");
    }



}
