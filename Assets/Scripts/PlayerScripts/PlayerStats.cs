using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int playerNum;
    public Transform gunPosSee;
    public Transform gunPosNotSee;
    public Weapons weapon;
    public Camera cam;
    private Camera arena_cam;
    [SerializeField]
    private GameObject player_body;
    [SerializeField]
    private GameObject player_hat;
    [SerializeField]
    private GameObject player_gun_notsee;
    [SerializeField]
    private GameObject player_gun_see;
    public bool allowPlayerMovement = false;
    public bool allowPlayerShoot = false;
    public bool allowPlayerRotate = false;
    public string team = "Red";
    public float sensitivityX = 100f;
    public float sensitivityY = 100f;
    public float minRotationSpeed = 1f;
    [HideInInspector]
    public float rotationSpeed = 1f;
    public float assistMultiplier = 1f;
    public float assistAngle = 25f;

    [SerializeField]
    private GameObject playerMesh1;
    [SerializeField]
    private GameObject playerMesh2;
    [SerializeField]
    private Material[] redTeamColors1;
    [SerializeField]
    private Material[] blueTeamColors1;
    [SerializeField]
    private Material[] redTeamColors2;
    [SerializeField]
    private Material[] blueTeamColors2;


#nullable enable
    public GameStateManager? gameState = null;
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

        if(arena_cam = GameObject.Find("Arena_Camera")?.GetComponent<Camera>()){
            for(int i = 1; i < 5; i ++){
                int layeri = LayerMask.NameToLayer("Gun" + i);
                arena_cam.cullingMask = arena_cam.cullingMask & ~(1 << layeri);
            }
        }

        UpdateRotationSpeed();
        UpdateSettingsVariables();
    }

    private void Init(Scene scene, LoadSceneMode mode)
    {
        gameState = GameObject.Find("GameManager")?.GetComponent<GameStateManager>();
        soccerBallBehavior = GameObject.FindWithTag("Soccer")?.GetComponentInChildren<SoccerBallBehavior>();
    }

    private void AssignTeam(){
        foreach(var mat in playerMesh1.GetComponent<Renderer>().materials){
            Debug.Log(mat.name);
        }

        if(team == "Red"){
            playerMesh1.GetComponent<Renderer>().materials = redTeamColors1;
            playerMesh2.GetComponent<Renderer>().materials = redTeamColors2;
        } else {
            playerMesh1.GetComponent<Renderer>().materials = blueTeamColors1;
            playerMesh2.GetComponent<Renderer>().materials = blueTeamColors2;
        }
    }

    public void AssignLayer(int playerNumVal){
        int playerLayer = LayerMask.NameToLayer("Player" + playerNumVal);

        Debug.Log("Layer of " + playerNum.ToString() + " : " + LayerMask.NameToLayer("Player" + playerNumVal).ToString());
        Debug.Log("Mask of " + playerNum.ToString() + " : " + cam.cullingMask.ToString());
        Debug.Log("change layer of " + playerNum.ToString() + " : " + ~(1 << playerLayer));

        for(int i = 1; i < 5; i ++){
            int layeri = LayerMask.NameToLayer("Gun" + i);
            if(playerNumVal == i){
                player_gun_see.layer = layeri;

                var children1 = player_gun_see.GetComponentsInChildren<Transform>(includeInactive: true);
                foreach (var child in children1){
                    child.gameObject.layer = layeri;
                }
            } else {
                cam.cullingMask = cam.cullingMask & ~(1 << layeri);
            }
        }

        cam.cullingMask = cam.cullingMask & ~(1 << playerLayer);
        player_body.layer = playerLayer;
        player_hat.layer = playerLayer;
        player_gun_notsee.layer = playerLayer;

        var children2 = player_gun_notsee.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children2){
            child.gameObject.layer = playerLayer;
        }

        Debug.Log(cam.cullingMask.ToString());
    }

    private void UpdateRotationSpeed()
    {
        if (soccerBallBehavior != null)
        {
            float distance = Vector3.Distance(soccerBallBehavior.GetPosition(), cam.transform.position);
            
            float angle = Vector3.Angle(cam.transform.forward, soccerBallBehavior.GetPosition() - cam.transform.position);
            float minSpeed = Mathf.Min(Mathf.Max(100 - distance, 0) / 4f * 0.013f * assistMultiplier + 0.25f, 1);

            rotationSpeed = (angle > assistAngle ? 1 : Mathf.Max(minSpeed, angle / assistAngle)) * minRotationSpeed;
        }
    }

    private void UpdateSettingsVariables()
    {
        List<float> playerSettings = new List<float>();
        if (playerNum == 1)
        {
            playerSettings = GameSettings.Player1Settings;
        } else if (playerNum == 2)
        {
            playerSettings = GameSettings.Player2Settings;
        } else if (playerNum == 3)
        {
            playerSettings = GameSettings.Player3Settings;
        } else
        {
            playerSettings = GameSettings.Player4Settings;
        }

        sensitivityX = playerSettings[0];
        sensitivityY = playerSettings[0];
        assistAngle = playerSettings[1];
        assistMultiplier = 2 - playerSettings[2];
    }
}
