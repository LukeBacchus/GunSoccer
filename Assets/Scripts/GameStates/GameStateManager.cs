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

    [SerializeField]
    private GameStats gameStats;
    [SerializeField]
    private TMPro.TextMeshProUGUI countDownText;
    [SerializeField]
    private TMPro.TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        introState = new IntroState();
        countdownState = new CountdownState(gameStats, countDownText);
        ongoingGameState = new OngoingGameState(gameStats, timerText);
        overtimeState = new OvertimeState(gameStats, timerText);
        goalState = new GoalState();
        gameOverState = new GameOverState();
        settingsState = new SettingsState(2);

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
