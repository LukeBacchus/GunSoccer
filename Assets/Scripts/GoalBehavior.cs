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

    [SerializeField] private GameObject[] players;
    private Vector3[] playerPoss = new Vector3[4];
    private Quaternion[] playerRots = new Quaternion[4];

    public int team;
    private GameStats gameStats;
<<<<<<< HEAD
    private GameStateManager gameState;
=======
>>>>>>> Audience_Behavior

    // Start is called before the first frame update
    private void Start()
    {
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();
<<<<<<< HEAD
        gameState = GameObject.Find("GameManager").GetComponent<GameStateManager>();
=======
>>>>>>> Audience_Behavior
        teamOneScoreText = GameObject.Find("Team 1 Score").GetComponent<TMPro.TextMeshProUGUI>();
        teamTwoScoreText = GameObject.Find("Team 2 Score").GetComponent<TMPro.TextMeshProUGUI>();

        for(int i = 0; i < players.Length; i++){
            playerPoss[i] = players[i].gameObject.transform.position;
            playerRots[i] = players[i].gameObject.transform.rotation;
        }

        Debug.Log("Pos init: " + playerPoss[0]);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Soccer" && gameState.currentState.stateType == GameStates.StateTypes.INGAME)
        {
<<<<<<< HEAD
=======
            collision.gameObject.transform.position = new Vector3(0, 5, 0);
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            Debug.Log("Pos: " + playerPoss[0]);

            for(int i = 0; i < players.Length; i++){
                players[i].gameObject.transform.position = playerPoss[i];
                players[i].gameObject.transform.rotation = playerRots[i];
            }

>>>>>>> Audience_Behavior
            if (team == 1)
            {
                gameStats.teamTwoScore += 1;
                teamTwoScoreText.text = "Team 2 Score: " + gameStats.teamTwoScore;
            } else
            {
                gameStats.teamOneScore += 1;
                teamOneScoreText.text = "Team 1 Score: " + gameStats.teamOneScore;
            }

<<<<<<< HEAD
            gameState.SwitchState(gameState.goalState);
=======

            StartCoroutine(gameStats.Countdown());
>>>>>>> Audience_Behavior
        }
    }

}
