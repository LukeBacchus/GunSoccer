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
    public PostGameState postGameState;
    public GoalState goalState;
    public GameOverState gameOverState;
    public SettingsState settingsState;

    public List<GameObject> players;

    [SerializeField]
    private GameStats gameStats;
    [SerializeField]
    private TMPro.TextMeshProUGUI countDownText;
    [SerializeField]
    private GameObject statsUI;
    [SerializeField]
    private GameObject winUI;
    [SerializeField]
    private GameObject twoPlayerUI;
    [SerializeField]
    private GameObject fourPlayerUI;
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private RectTransform scoreBoard;
    [SerializeField]
    private GameObject blackScreen;
    [SerializeField]
    private GameObject announcement;
    [SerializeField]
    private InitializeMap initMap;
    [SerializeField]
    private SoccerBallBehavior soccerBallBehavior;
    [SerializeField]
    private StadiumCamera stadiumCamera;
    public GameObject[] arrows;

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
        introState = new IntroState(twoPlayerUI, fourPlayerUI, gameUI, stadiumCamera, blackScreen);
        countdownState = new CountdownState(gameStats, countDownText, initMap, soccerBallBehavior);
        ongoingGameState = new OngoingGameState(gameStats);
        overtimeState = new OvertimeState();
        postGameState = new PostGameState(gameStats, announcement);
        goalState = new GoalState(gameStats, soccerBallBehavior);
        gameOverState = new GameOverState(winUI, statsUI);
        settingsState = new SettingsState(2);

        gameStats.UpdateTimerUI();
        SetScoreBoardLocation();

        statsUI.SetActive(false);
        winUI.SetActive(false);
        twoPlayerUI.SetActive(false);
        fourPlayerUI.SetActive(false);
        gameUI.SetActive(false);
        soccerBallBehavior.DisableGravity();

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

    private void SetScoreBoardLocation()
    {
        if (players.Count == 2)
        {
            scoreBoard.anchorMin = new Vector2(0, 1);
            scoreBoard.anchorMax = new Vector2(1, 1);
            scoreBoard.anchoredPosition = new Vector3(0, -scoreBoard.rect.height / 2, 0);
        }
        else
        {
            scoreBoard.anchorMin = new Vector2(0, 0.5f);
            scoreBoard.anchorMax = new Vector2(1, 0.5f);
            scoreBoard.anchoredPosition = Vector3.zero;
        }
    }
}
