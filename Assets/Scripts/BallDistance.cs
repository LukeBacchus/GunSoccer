using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDistance : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ball; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Dist to ball: " + Vector3.Distance(player.transform.position, ball.transform.position));
    }
}
