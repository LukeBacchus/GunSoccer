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
    private double lastInterval;
    private int frames;

    void Awake()
    {
        teamOneScore = 0;
        teamTwoScore = 0;
        gameTime = 3 * 60;
    }

    public bool TimeIsUp()
    {
        return gameTime <= 0;
    }

    public bool ScoreTied()
    {
        return teamOneScore == teamTwoScore;
    }
}
