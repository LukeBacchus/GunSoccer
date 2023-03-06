using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int playerNum;
    public Weapons weapon;
    public Camera cam;
    public GameObject camParent;
    public bool allowPlayerInput = false;

#nullable enable
    private GameStats? gameStats = null;

    private void Start()
    {
        gameStats = GameObject.Find("GameManager")?.GetComponent<GameStats>();

        SceneManager.sceneLoaded += Init;
    }

    private void Update()
    {
        if (gameStats != null)
        {
            allowPlayerInput = gameStats.gameStatus != GameStats.GameStatus.PAUSED;
        }
    }

    private void Init(Scene scene, LoadSceneMode mode)
    {
        gameStats = GameObject.Find("GameManager")?.GetComponent<GameStats>();
    }
}
