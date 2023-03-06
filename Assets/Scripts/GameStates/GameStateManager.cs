using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameStates currentState;
    public GameStates prevState;

    public IntroState introState;
    public CountdownState countdownState;
    public OngoingGameState ongoingGameState;
    public OvertimeState overtimeState;
    public GoalState goalState;
    public GameOverState gameOverState;
    public SettingsState settingsState;

    public List<GameObject> players;

    [SerializeField]
    private GameStats gameStats;
    [SerializeField]
    private TMPro.TextMeshProUGUI countDownText;
    [SerializeField]
    private TMPro.TextMeshProUGUI timerText;
    [SerializeField]
    private GameObject winUI;
    [SerializeField]
    private GameObject twoPlayerUI;
    [SerializeField]
    private GameObject fourPlayerUI;
    [SerializeField]
    private GameObject scoreBoard;
    [SerializeField]
    private GameObject blackScreen;
    [SerializeField]
    private InitializeMap initMap;
    [SerializeField]
    private SoccerBallBehavior soccerBallBehavior;
    [SerializeField]
    private Rigidbody soccerBallRB;
    [SerializeField]
    private StadiumCamera stadiumCamera;

    void Awake()
    {
        for (int i = 1; i <= 4; i++)
        {
            GameObject player = GameObject.Find("Player " + i);
            if (player == null) break;
            players.Add(player);
        }

        initMap.ResetPlayerLocs(players);
        initMap.MovePlayersIntoScene(players);
    }

    // Start is called before the first frame update
    void Start()
    {
        introState = new IntroState(twoPlayerUI, fourPlayerUI, scoreBoard, stadiumCamera, blackScreen);
        countdownState = new CountdownState(gameStats, countDownText, initMap, soccerBallBehavior);
        ongoingGameState = new OngoingGameState(gameStats, timerText);
        overtimeState = new OvertimeState();
        goalState = new GoalState(gameStats, soccerBallRB);
        gameOverState = new GameOverState(winUI);
        settingsState = new SettingsState(2);

        timerText.text = "Timer: " + string.Format("{0:0}:{1:00}", Mathf.FloorToInt(gameStats.gameTime / 60), Mathf.FloorToInt(gameStats.gameTime % 60));
        winUI.SetActive(false);
        twoPlayerUI.SetActive(false);
        fourPlayerUI.SetActive(false);
        scoreBoard.SetActive(false);

        currentState = introState;
        prevState = null;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameStates nextState)
    {
        prevState = currentState;
        currentState = nextState;
        currentState.EnterState(this);
    }
}
