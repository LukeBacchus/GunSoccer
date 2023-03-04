using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public GameStatus gameStatus = GameStatus.COUNTDOWN;
    public int teamOneScore;
    public int teamTwoScore;
    public float gameTime;
    public bool setOT;
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames;
    private GameObject winUI;
    private string overtimeText = "";
    private TMPro.TextMeshProUGUI timerText;
    private TMPro.TextMeshProUGUI countDownText;
    [SerializeField]
    private InitializeMap mapInit;


    public enum GameStatus
    {
        COUNTDOWN, 
        ONGOING,
        OVERTIME
    }

    // Start is called before the first frame update
    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;

        teamOneScore = 0;
        teamTwoScore = 0;
        gameTime = 5*30;
        timerText = GameObject.Find("Timer").GetComponent<TMPro.TextMeshProUGUI>();
        timerText.text = "Timer: " + string.Format("{0:0}:{1:00}", Mathf.FloorToInt(gameTime / 60), Mathf.FloorToInt(gameTime % 60));
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
        Debug.Log(gameStatus);
        if (gameTime > 0) {
            if (gameStatus != GameStatus.COUNTDOWN) {
                gameTime -= Time.deltaTime;
                timerText.text = "Timer: " + string.Format("{0:0}:{1:00}", Mathf.FloorToInt(gameTime / 60), Mathf.FloorToInt(gameTime % 60));
            }
        }
        else if (teamOneScore == teamTwoScore && setOT == false) {
            setOT = true;
            timerText.text = "Overtime!";
            mapInit.ResetPlayerLocs();
            GameObject ball = GameObject.Find("pasted__Soccer_Ball");
            ball.transform.position = new Vector3(0, 5, 0);
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameStatus = GameStatus.OVERTIME;
            StartCoroutine(Countdown());
        }
        else if (teamOneScore != teamTwoScore) {
            winUI.SetActive(true);
        }
    }
    void ResumeGame ()
    {
        gameStatus = GameStatus.ONGOING;
        Time.timeScale = 1;
    }
    
    void ResumeOverTime ()
    {
        gameStatus = GameStatus.OVERTIME;
        Time.timeScale = 1;
    }

    public IEnumerator Countdown(){
        gameStatus = GameStatus.COUNTDOWN;
        if (gameTime <= 0) {
            overtimeText = "OVERTIME: ";
        }
        float countDownTime = 5;
        while (countDownTime > 0) {
            countDownTime -= Time.deltaTime;
            countDownText.text = overtimeText + Mathf.Round(countDownTime); 
            yield return null;
        }
        countDownText.text = "";
        if (gameTime <= 0) {
            ResumeOverTime();
        }
        else {
            ResumeGame();
        }
    }
}
