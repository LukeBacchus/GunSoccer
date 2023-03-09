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

    [SerializeField]
    private GameObject playerMesh;
    [SerializeField]
    private Material[] redTeamColors;
    [SerializeField]
    private Material[] blueTeamColors;

#nullable enable
    private GameStateManager? gameState = null;

    private void Start()
    {
        gameState = GameObject.Find("GameManager")?.GetComponent<GameStateManager>();
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
    }

    private void Init(Scene scene, LoadSceneMode mode)
    {
        gameState = GameObject.Find("GameManager")?.GetComponent<GameStateManager>();
    }

    private void AssignTeam(){
        if(team == "Red"){
            playerMesh.GetComponent<Renderer>().materials = redTeamColors;
        } else {
            playerMesh.GetComponent<Renderer>().materials = blueTeamColors;
        }
    }
}
