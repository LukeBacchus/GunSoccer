using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalBehavior : MonoBehaviour
{
    private TMPro.TextMeshProUGUI teamOneScoreText;
    private TMPro.TextMeshProUGUI teamTwoScoreText;

    public int team;
    private Scores scores;

    // Start is called before the first frame update
    private void Start()
    {
        scores = GameObject.Find("GameManager").GetComponent<Scores>();
        teamOneScoreText = GameObject.Find("Team 1 Score").GetComponent<TMPro.TextMeshProUGUI>();
        teamTwoScoreText = GameObject.Find("Team 2 Score").GetComponent<TMPro.TextMeshProUGUI>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Soccer")
        {
            collision.gameObject.transform.position = new Vector3(0, 5, 0);

            if (team == 1)
            {
                scores.teamTwoScore += 1;
                teamTwoScoreText.text = "Team 2 Score: " + scores.teamTwoScore;
            } else
            {
                scores.teamOneScore += 1;
                teamOneScoreText.text = "Team 1 Score: " + scores.teamOneScore;
            }
        }
    }
}
