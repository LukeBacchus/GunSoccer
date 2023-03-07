using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BallDistance : MonoBehaviour
{

    public StudioEventEmitter ambience;


    [SerializeField] private GameObject goal;
    [SerializeField] private GameObject ball;
  

    // Start is called before the first frame update
    void Start()
    {
        ambience = GetComponent<StudioEventEmitter>();
        ambience.Play();
        
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(goal.transform.position, ball.transform.position);

        Debug.Log("Dist to ball: " + distance);

        if (distance < 20)
        {
            ambience.SetParameter("Intensity", 100);


        }

        if (distance > 20)
        {
            ambience.SetParameter("Intensity", 0);


        }

    }
}



  