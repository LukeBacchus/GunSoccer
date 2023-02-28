using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinStats: MonoBehaviour 
{
    private TMPro.TextMeshProUGUI winnerText;
    public float timer;
    void Start () 
    {
        Debug.Log(Scores.TeamScores);
        winnerText = GameObject.Find("Win Text").GetComponent<TMPro.TextMeshProUGUI>();
        timer = 15;
        winnerText.text = Scores.TeamScores + " Won!!! Congrats!!! Returning To Main Menu In: 15";
    }
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            winnerText.text = Scores.TeamScores + " Won!!! Congrats!!! Returning To Main Menu In: " + System.Math.Round(timer).ToString();
        } else {
            SceneManager.LoadScene("Start Screen"); 
        }

    }


}
