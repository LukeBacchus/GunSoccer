using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalBehavior : MonoBehaviour
{
    private TMPro.TextMeshProUGUI teamOneScoreText;
    private TMPro.TextMeshProUGUI teamTwoScoreText;
    [SerializeField]
    private InitializeMap mapInit;

    public int team;
    private GameStats gameStats;
    private GameStateManager gameState;

    // Start is called before the first frame update
    private void Start()
    {
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();
        gameState = GameObject.Find("GameManager").GetComponent<GameStateManager>();
        teamOneScoreText = GameObject.Find("Team 1 Score").GetComponent<TMPro.TextMeshProUGUI>();
        teamTwoScoreText = GameObject.Find("Team 2 Score").GetComponent<TMPro.TextMeshProUGUI>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Soccer" && gameState.currentState.stateType == GameStates.StateTypes.INGAME)
        {
            if (team == 1)
            {
                gameStats.teamTwoScore += 1;
                teamTwoScoreText.text = "Team 2 Score: " + gameStats.teamTwoScore;
            } else
            {
                gameStats.teamOneScore += 1;
                teamOneScoreText.text = "Team 1 Score: " + gameStats.teamOneScore;
            }

            gameState.SwitchState(gameState.goalState);
        }
    }

}
