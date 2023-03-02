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
    void Start () 
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
        timer = 15;
        winnerText.text = winner + " Won!!! Congrats!!! Returning To Main Menu In: 15";
    }
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            winnerText.text = winner + " Won!!! Congrats!!! Returning To Main Menu In: " + System.Math.Round(timer).ToString();
        } else {
            SceneManager.LoadScene("Start Screen"); 
        }

    }


}
