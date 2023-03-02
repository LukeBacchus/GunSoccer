using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public GameStatus gameStatus = GameStatus.PAUSED;
    public int teamOneScore;
    public int teamTwoScore;
    public float gameTime;
    public float updateInterval = 0.5F;
    public bool overtime = false;
    private double lastInterval;
    private int frames;
    private GameObject winUI;
    
    private TMPro.TextMeshProUGUI timerText;
    private TMPro.TextMeshProUGUI countDownText;
    [SerializeField]
    private InitializeMap mapInit;


    public enum GameStatus
    {
        ONGOING,
        PAUSED,
        OVERTIME
    }

    // Start is called before the first frame update
    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;

        teamOneScore = 0;
        teamTwoScore = 0;
        gameTime = 5*30 + 1;
        timerText = GameObject.Find("Timer").GetComponent<TMPro.TextMeshProUGUI>();
        countDownText = GameObject.Find("CountDown").GetComponent<TMPro.TextMeshProUGUI>();
        winUI = GameObject.Find("WinScreen");
        winUI.SetActive(false);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Debug.Log(players.Length);

        if(players.Length == 2){
            GameObject four_playerUI = GameObject.Find("2v2 UI");
            four_playerUI.SetActive(false);
        } else {
            GameObject two_playerUI = GameObject.Find("1v1 UI");
            two_playerUI.SetActive(false);
        }

        StartCoroutine(Countdown());

    }
    void Update()
    {
        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            timerText.text = "Timer: " + string.Format("{0:0}:{1:00}", Mathf.FloorToInt(gameTime / 60), Mathf.FloorToInt(gameTime % 60));
        } 
        else if (teamOneScore == teamTwoScore && !overtime) {
            overtime = true;
            mapInit.ResetPlayerLocs();
            GameObject ball = GameObject.Find("pasted__Soccer_Ball");
            ball.transform.position = new Vector3(0, 5, 0);
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            StartCoroutine(Countdown(true));
            gameTime += 10 + 1;
            timerText.text = "Timer: " + string.Format("{0:0}:{1:00}", Mathf.FloorToInt(gameTime / 60), Mathf.FloorToInt(gameTime % 60));
        }
        else {
            if (teamOneScore > teamTwoScore) {
                Scores.TeamScores = "Team 1";
            }
            else if (teamTwoScore > teamOneScore) {
                Scores.TeamScores = "Team 2";
            }
            else {
                Scores.TeamScores = "No One";
            }
            winUI.SetActive(true);
        }

    }

    void PauseGame ()
    {
        gameStatus = GameStatus.PAUSED;
        Time.timeScale = 0;
    }
    
    void ResumeGame ()
    {
        gameStatus = GameStatus.ONGOING;
        Time.timeScale = 1;
    }
    
    public IEnumerator Countdown(bool overtime = false){
        if (overtime) {
            PauseGame();
            int count = 5;
            int i = count;
            float startTime = UnityEngine.Time.realtimeSinceStartup;
            while (UnityEngine.Time.realtimeSinceStartup - startTime < count + 1) {
                countDownText.text = "OVERTIME: " + i.ToString();
                i = count - (int)(UnityEngine.Time.realtimeSinceStartup - startTime);
            yield return null;
            }
            countDownText.text = "";
            ResumeGame();
        }
        else {
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


}
