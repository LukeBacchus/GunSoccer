using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BallDistance : MonoBehaviour
{
    [SerializeField] private GameObject goal;
    [SerializeField] private GameObject ball;
    public FMODUnity.EventReference cheer;

    private FMOD.Studio.EventInstance instance;
    private float dist_to_goal;


    // Start is called before the first frame update
    void Start()
    {
       instance = FMODUnity.RuntimeManager.CreateInstance(cheer);
       instance.setParameterByName("Intensity", 0);

    }

    // Update is called once per frame
    void Update()
    {
        dist_to_goal = Vector3.Distance(goal.transform.position, ball.transform.position);
        Debug.Log("distance is " + dist_to_goal);

        if (dist_to_goal < 50)
        {
            
            instance.setParameterByName("Intensity", 100);
            RuntimeManager.PlayOneShot("event:/cheer");

            if (dist_to_goal > 50)
            {
                instance.setParameterByName("Intensity", 0);
            }

    

        } 
    }
}
