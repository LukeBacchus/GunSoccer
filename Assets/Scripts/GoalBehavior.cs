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

    // Start is called before the first frame update
    private void Start()
    {
        gameStats = GameObject.Find("GameManager").GetComponent<GameStats>();
        teamOneScoreText = GameObject.Find("Team 1 Score").GetComponent<TMPro.TextMeshProUGUI>();
        teamTwoScoreText = GameObject.Find("Team 2 Score").GetComponent<TMPro.TextMeshProUGUI>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Soccer")
        {
            collision.gameObject.transform.position = new Vector3(0, 5, 0);
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            if (team == 1)
            {
                gameStats.teamTwoScore += 1;
                teamTwoScoreText.text = "Team 2 Score: " + gameStats.teamTwoScore;
            } else
            {
                gameStats.teamOneScore += 1;
                teamOneScoreText.text = "Team 1 Score: " + gameStats.teamOneScore;
            }
            if (gameStats.gameStatus != GameStats.GameStatus.OVERTIME) {
                mapInit.ResetPlayerLocs();
                StartCoroutine(gameStats.Countdown());
            }
        }
    }

}
