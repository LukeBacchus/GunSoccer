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

    public void ResetPlayerLocs(List<GameObject> players)
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
            players[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            players[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    public void MovePlayersIntoScene(List<GameObject> players)
    {
        foreach (GameObject player in players)
        {
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetActiveScene());
        }
    }
}
