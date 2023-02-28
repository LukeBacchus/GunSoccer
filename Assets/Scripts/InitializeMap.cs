using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeMap : MonoBehaviour
{
    [SerializeField]
    List<Transform> twoPlayerSpawns;

    [SerializeField]
    List<Transform> fourPlayerSpawns;

    private List<GameObject> players = new List<GameObject>();
    void Awake()
    {
        for (int i = 1; i <= 4; i++)
        {
            GameObject player = GameObject.Find("Player " + i);
            if (player == null) break;
            players.Add(player);
        }

        ResetPlayerLocs();
        MovePlayersIntoScene();
    }

    public void ResetPlayerLocs()
    {
        List<Transform> spawnLocations = new List<Transform>();
        if (players.Count == 2)
        {
            spawnLocations = twoPlayerSpawns;
        }
        else if (players.Count == 4)
        {
            spawnLocations = fourPlayerSpawns;
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawnLocations[i].position + new Vector3(0, 1, 0);
            players[i].transform.rotation = spawnLocations[i].rotation;
        }
    }

    private void MovePlayersIntoScene()
    {
        foreach (GameObject player in players)
        {
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetActiveScene());
        }
    }
}
