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
    
    private TMPro.TextMeshProUGUI timerText;
    private TMPro.TextMeshProUGUI countDownText;

    // Start is called before the first frame update
    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;

        teamOneScore = 0;
        teamTwoScore = 0;
        gameTime = 5*60 + 1;
        timerText = GameObject.Find("Timer").GetComponent<TMPro.TextMeshProUGUI>();
        countDownText = GameObject.Find("CountDown").GetComponent<TMPro.TextMeshProUGUI>();

        StartCoroutine(Countdown());

    }
    void Update()
    {
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            timerText.text = "Timer: " + string.Format("{0:0}:{1:00}", Mathf.FloorToInt(gameTime / 60), Mathf.FloorToInt(gameTime % 60));
        } else {
            SceneManager.LoadScene("WinScreen"); 
        }

    }

    void PauseGame ()
    {
        Time.timeScale = 0;
    }
    
    void ResumeGame ()
    {
        Time.timeScale = 1;
    }
    
    public IEnumerator Countdown(){
        PauseGame();
        int count = 5;
        int i = count;
        float startTime = UnityEngine.Time.realtimeSinceStartup;
        while (UnityEngine.Time.realtimeSinceStartup - startTime < count + 1) {
            countDownText.text = i.ToString();
            i = count - (int)(UnityEngine.Time.realtimeSinceStartup - startTime);
            yield return null;
        }

        countDownText.text = "";

        ResumeGame();
    }


}
