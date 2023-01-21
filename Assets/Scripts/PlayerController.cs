using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerNum;
    [SerializeField] private float speed = 2.0F;
    private Rigidbody rb;
    [SerializeField] private GameObject CameraObj;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal" + (playerNum).ToString());
        float z = Input.GetAxisRaw("Vertical" + (playerNum).ToString());
        rb.velocity = (x * transform.right + z * transform.forward) * speed;
    }

    void Rotate(){
        Vector3 rot = CameraObj.GetComponent<CameraBehavior>().transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(0, rot.y, 0);
    }
}
