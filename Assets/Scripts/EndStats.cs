using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndStats: MonoBehaviour 
{
    private TMPro.TextMeshProUGUI team1ScoreText;
    private TMPro.TextMeshProUGUI team2ScoreText;
    private TMPro.TextMeshProUGUI team1WinText;
    private TMPro.TextMeshProUGUI team2WinText;

    public float timer;
    private GameStats gameStats;
    // private string team1ScoreText;
    // private string team2ScoreText;
    // private string team1WinText;
    // private string team2WinText;

    public void DisplayStats () {
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();
        team1ScoreText = GameObject.Find("Team1Score").GetComponent<TMPro.TextMeshProUGUI>();
        team1ScoreText.text = gameStats.teamOneScore.ToString();
        team2ScoreText = GameObject.Find("Team2Score").GetComponent<TMPro.TextMeshProUGUI>();
        team2ScoreText.text = gameStats.teamTwoScore.ToString();
        team1WinText = GameObject.Find("Team1Win").GetComponent<TMPro.TextMeshProUGUI>();
        team2WinText = GameObject.Find("Team2Win").GetComponent<TMPro.TextMeshProUGUI>();

        if (gameStats.teamOneScore > gameStats.teamTwoScore) {
            team1WinText.text = "Red";
            team2WinText.text = "";
        }
        else {
            team2WinText.text = "Blue";
            team1WinText.text = "";
        }
    }

    public void UpdateStats() {

    }

}
