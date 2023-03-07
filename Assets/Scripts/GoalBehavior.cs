using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalBehavior : MonoBehaviour
{
    public int team;
    private GameStats gameStats;
    private GameStateManager gameState;

    // Start is called before the first frame update
    private void Start()
    {
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();
        gameState = GameObject.Find("GameManager").GetComponent<GameStateManager>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Soccer" && gameState.currentState.stateType == GameStates.StateTypes.INGAME)
        {
            if (team == 1)
            {
                gameStats.teamTwoScore += 1;
            } else
            {
                gameStats.teamOneScore += 1;
            }
            gameStats.UpdateScoresUI();

            gameState.SwitchState(gameState.goalState);
        }
    }

}
