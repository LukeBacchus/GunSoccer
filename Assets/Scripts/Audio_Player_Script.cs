using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Audio_Player_Script : MonoBehaviour
{

    public StudioEventEmitter Ambience;
    public StudioEventEmitter Music;

    // Start is called before the first frame update
    void Start()
    {

        Ambience = GetComponent<StudioEventEmitter>();
        Ambience.Play();

        Music = GetComponent<StudioEventEmitter>();
        Music.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

