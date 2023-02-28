using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMOD_Script : MonoBehaviour
{

    public StudioEventEmitter sfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stadium Surface")
        {
            RuntimeManager.PlayOneShot("event:/Ball Collision");
        }
    }
}
