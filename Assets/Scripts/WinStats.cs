using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinStats: MonoBehaviour 
{
    private TMPro.TextMeshProUGUI winnerText;
    public float timer;
    private GameStats gameStats;
    private string winner;

    public void DisplayWinner () 
    {
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();
        if (gameStats.teamOneScore > gameStats.teamTwoScore) {
            winner = "Team 1";
        }
        else if (gameStats.teamTwoScore > gameStats.teamOneScore) {
            winner = "Team 2";
        }
        else {
            winner = "No One";
        }
        winnerText = GameObject.Find("Win Text").GetComponent<TMPro.TextMeshProUGUI>();
        timer = 15.99f;
        winnerText.text = winner + " Won!!! Congrats!!! Returning To Main Menu In: 15";

        StartCoroutine(UpdateWinScreen());
    }
    private IEnumerator UpdateWinScreen()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            winnerText.text = winner + " Won!!! Congrats!!! Returning To Main Menu In: " + (int) timer;
            yield return null;
        }

        SceneManager.LoadScene("Start Screen"); 
    }


}
