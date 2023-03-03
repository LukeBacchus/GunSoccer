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
    private double lastInterval;
    private int frames;
    private float currentGameTime;
    private GameObject winUI;
    private string overtimeText = "";
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
        gameTime = 5; //5*30 + 1;
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
        // Debug.Log(gameStatus);
        if (teamOneScore != teamTwoScore && gameStatus == GameStatus.OVERTIME) {
            winUI.SetActive(true);
        }
        else if (gameTime > 0 && gameStatus != GameStatus.OVERTIME)
        {
            gameTime -= Time.deltaTime;
            timerText.text = "Timer: " + string.Format("{0:0}:{1:00}", Mathf.FloorToInt(gameTime / 60), Mathf.FloorToInt(gameTime % 60));
        } 
        else if (gameTime > 0 && gameStatus == GameStatus.OVERTIME)
        {
            gameTime -= Time.deltaTime;
            timerText.text = "Overtime!"; // We can have a timer too, or just display Overtime- not sure which is the best way
            // + string.Format("{0:0}:{1:00}", Mathf.FloorToInt(gameTime / 60), Mathf.FloorToInt(gameTime % 60));
        } 
        else if (teamOneScore == teamTwoScore && gameStatus != GameStatus.OVERTIME) {
            gameStatus = GameStatus.OVERTIME;
            mapInit.ResetPlayerLocs();
            GameObject ball = GameObject.Find("pasted__Soccer_Ball");
            ball.transform.position = new Vector3(0, 5, 0);
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            StartCoroutine(Countdown());
            gameTime = 60;
            timerText.text = "Overtime: " + string.Format("{0:0}:{1:00}", Mathf.FloorToInt(gameTime / 60), Mathf.FloorToInt(gameTime % 60));
            gameStatus = GameStatus.OVERTIME;
        }
        else {
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
    
    void ResumeOverTime ()
    {
        gameStatus = GameStatus.OVERTIME;
        Time.timeScale = 1;
    }

    public IEnumerator Countdown(){
        currentGameTime = gameTime;
        if (gameStatus == GameStatus.OVERTIME) {
            overtimeText = "OVERTIME: ";
        }
        PauseGame();
        int count = 5;
        int i = count;
        float startTime = UnityEngine.Time.realtimeSinceStartup;
        while (UnityEngine.Time.realtimeSinceStartup - startTime < count + 1) {
            countDownText.text = overtimeText + i.ToString();
            i = count - (int)(UnityEngine.Time.realtimeSinceStartup - startTime);
            yield return null;
        }
        countDownText.text = "";
        if (currentGameTime <= 0) {
            ResumeOverTime();
        } else {
            ResumeGame();
        }
    }
}
