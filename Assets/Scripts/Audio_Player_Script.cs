using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Audio_Player_Script : MonoBehaviour
{
    [SerializeField] private GameObject goal1;
    [SerializeField] private GameObject goal2;
    [SerializeField] private GameObject ball;

    private float dist_to_goal1;
    private float dist_to_goal2;

    public StudioEventEmitter Ambience;
    public StudioEventEmitter Music;

    // Start is called before the first frame update
    void Start()
    {

        Ambience = GetComponent<StudioEventEmitter>();
        Ambience.Play();
        Ambience.SetParameter("cheer_level", 0);

        Music = GetComponent<StudioEventEmitter>();
        Music.Play();

    }

    // Update is called once per frame
    void Update()
    {
        dist_to_goal1 = Vector3.Distance(goal1.transform.position, ball.transform.position);
        dist_to_goal2 = Vector3.Distance(goal2.transform.position, ball.transform.position);

        //Debug.Log("goal 1 " + dist_to_goal1);
        //Debug.Log("goal 2 " + dist_to_goal2);

        if (dist_to_goal2 < 70)
        {

            Ambience.SetParameter("cheer_level", 100);

             
        }

     


        if (dist_to_goal1 < 70)
        {

            Ambience.SetParameter("cheer_level", 100);

           
        }

        if (dist_to_goal1 > 70 && dist_to_goal2 > 70)
        {
            Ambience.SetParameter("cheer_level", 0);
        }








    }
}

