using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BallDistance : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    [SerializeField] private GameObject ball;
    [SerializeField] private StudioEventEmitter soundEmitter;
    [SerializeField] private string Intensity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(goal.transform.position, ball.transform.position);

        soundEmitter.SetParameter(Intensity, distance);

        Debug.Log("Dist to ball: " + distance);
    }
}
