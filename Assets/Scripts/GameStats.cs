using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{

    public int teamOneScore;
    public int teamTwoScore;
    public float gameTime;
    // Start is called before the first frame update
    private TMPro.TextMeshProUGUI timerText;
    void Start()
    {
        teamOneScore = 0;
        teamTwoScore = 0;
        gameTime = 5*60;
        timerText = GameObject.Find("Timer").GetComponent<TMPro.TextMeshProUGUI>();
    }
    void Update()
    {
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            timerText.text = "Timer: " + Mathf.FloorToInt(gameTime / 60) + ":" + Mathf.FloorToInt(gameTime % 60);

        }

    }
}
