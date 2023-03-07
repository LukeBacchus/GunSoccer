using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public int teamOneScore;
    public int teamTwoScore;
    public float gameTime;
    public float updateInterval = 0.5F;

    [SerializeField]
    private TMPro.TextMeshProUGUI teamOneScoreText;
    [SerializeField]
    private TMPro.TextMeshProUGUI teamTwoScoreText;
    [SerializeField]
    private TMPro.TextMeshProUGUI timerText;

    private double lastInterval;
    private int frames;

    void Awake()
    {
        teamOneScore = 0;
        teamTwoScore = 0;
        gameTime = 10;
    }

    public bool TimeIsUp()
    {
        return gameTime <= 0;
    }

    public bool ScoreTied()
    {
        return teamOneScore == teamTwoScore;
    }

    public void UpdateScoresUI()
    {
        teamOneScoreText.text = teamOneScore.ToString();
        teamTwoScoreText.text = teamTwoScore.ToString();
    }

    public void UpdateTimerUI()
    {
        gameTime = Mathf.Max(gameTime, 0);
        timerText.text = string.Format("{0:0}:{1:00}", Mathf.FloorToInt(gameTime / 60), Mathf.FloorToInt(gameTime % 60));
        if (gameTime <= 10)
        {
            timerText.color = Color.red;
        }
    }
}
