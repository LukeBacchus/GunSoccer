using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int playerNum;
    public Weapons weapon;
    public Camera cam;
    public bool allowPlayerMovement = false;
    public bool allowPlayerShoot = false;
    public bool allowPlayerRotate = false;
    public string team = "Red";
    public float sensitivityX = 100f;
    public float sensitivityY = 100f;
    public float rotationSpeed = 1f;

    [SerializeField]
    private GameObject playerMesh;
    [SerializeField]
    private Material[] redTeamColors;
    [SerializeField]
    private Material[] blueTeamColors;

#nullable enable
    private GameStateManager? gameState = null;
    public SoccerBallBehavior? soccerBallBehavior = null;

    private void Start()
    {
        gameState = GameObject.Find("GameManager")?.GetComponent<GameStateManager>();
        soccerBallBehavior = GameObject.FindWithTag("Soccer")?.GetComponentInChildren<SoccerBallBehavior>();
        AssignTeam();
        SceneManager.sceneLoaded += Init;
    }

    private void Update()
    {
        if (gameState != null)
        {
            allowPlayerMovement = gameState.currentState.stateType == GameStates.StateTypes.INGAME;
            allowPlayerShoot = gameState.currentState.stateType == GameStates.StateTypes.INGAME;
            allowPlayerRotate = gameState.currentState.stateType == GameStates.StateTypes.PREGAME || gameState.currentState.stateType == GameStates.StateTypes.INGAME;
        }

        UpdateRotationSpeed();
    }

    private void Init(Scene scene, LoadSceneMode mode)
    {
        gameState = GameObject.Find("GameManager")?.GetComponent<GameStateManager>();
        soccerBallBehavior = GameObject.FindWithTag("Soccer")?.GetComponentInChildren<SoccerBallBehavior>();
    }

    private void AssignTeam(){
        if(team == "Red"){
            playerMesh.GetComponent<Renderer>().materials = redTeamColors;
        } else {
            playerMesh.GetComponent<Renderer>().materials = blueTeamColors;
        }
    }

    private void UpdateRotationSpeed()
    {
        if (soccerBallBehavior != null)
        {
            float distance = Vector3.Distance(soccerBallBehavior.GetPosition(), cam.transform.position);
            
            float angle = Vector3.Angle(cam.transform.forward, soccerBallBehavior.GetPosition() - cam.transform.position);
            float minSpeed = Mathf.Max(100 - distance, 0) / 4f * 0.013f + 0.25f;

            rotationSpeed = angle > 25 ? 1 : Mathf.Max(minSpeed, angle / 25);
        }
    }
}
