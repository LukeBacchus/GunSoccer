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
    private string team1Score;
    private string team2Score;
    private string team1Win;
    private string team2Win;

    public void DisplayStats () {
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();
        team1ScoreText = GameObject.Find("Team1Score").GetComponent<TMPro.TextMeshProUGUI>();
        team1ScoreText.text = "Team 1 Score: " + gameStats.teamOneScore;
        team2ScoreText = GameObject.Find("Team2Score").GetComponent<TMPro.TextMeshProUGUI>();
        team2ScoreText.text = "Team 2 Score: " + gameStats.teamTwoScore;
        team1WinText = GameObject.Find("Team1Win").GetComponent<TMPro.TextMeshProUGUI>();
        team2WinText = GameObject.Find("Team2Win").GetComponent<TMPro.TextMeshProUGUI>();

        if (gameStats.teamOneScore > gameStats.teamTwoScore) {
            team1WinText.text = "Team 1 Wins!";
            team2WinText.text = "";
        }
        else {
            team2WinText.text = "Team 2 Wins!";
            team1WinText.text = "";
        }
    }

    public void UpdateStats() {

    }

}
