using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndStats: MonoBehaviour 
{
    [SerializeField]
    private TMPro.TextMeshProUGUI score;
    [SerializeField]
    private GameObject team1WinText;
    [SerializeField]
    private GameObject team2WinText;

    private GameStats gameStats;

    public void DisplayStats () {
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();

        score.text = "<color=red>" + gameStats.teamOneScore.ToString() + "</color> : <color=blue>" + gameStats.teamTwoScore.ToString() + "</color>";

        if (gameStats.teamOneScore > gameStats.teamTwoScore) {
            team1WinText.SetActive(true);
            team2WinText.SetActive(false);
        }
        else {
            team2WinText.SetActive(true);
            team1WinText.SetActive(false);
        }
    }

}
