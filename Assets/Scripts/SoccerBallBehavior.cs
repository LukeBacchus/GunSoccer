using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBallBehavior : MonoBehaviour
{
    private Vector3 initialPosition = new Vector3(0, 5, 0);

    // Start is called before the first frame update
    void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
