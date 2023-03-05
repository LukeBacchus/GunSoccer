using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public int teamOneScore;
    public int teamTwoScore;
    public float gameTime;
    //public bool setOT;
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames;

    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;

        teamOneScore = 0;
        teamTwoScore = 0;
        gameTime = 5 * 60;
    }
}
