using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndStats: MonoBehaviour 
{
    private TMPro.TextMeshProUGUI team1ScoreText;
    private TMPro.TextMeshProUGUI team2ScoreText;
    private TMPro.TextMeshProUGUI teamWinText;

    public float timer;
    private GameStats gameStats;
    private string team1Score;
    private string team2Score;
    private string team1Win;
    private string team2Win;

    public void DisplayStats () {
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();
        team1ScoreText = GameObject.Find("Team1Score").GetComponent<TMPro.TextMeshProUGUI>();
        team1ScoreText.text = "" + gameStats.teamOneScore;
        team2ScoreText = GameObject.Find("Team2Score").GetComponent<TMPro.TextMeshProUGUI>();
        team2ScoreText.text = "" + gameStats.teamTwoScore;
        teamWinText = GameObject.Find("TeamWin").GetComponent<TMPro.TextMeshProUGUI>();

        if (gameStats.teamOneScore > gameStats.teamTwoScore) {
            teamWinText.text = "1";
        }
        else {
            teamWinText.text = "2";
        }
    }

    public void UpdateStats() {

    }

}
