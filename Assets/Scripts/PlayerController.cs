using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerNum;
    [SerializeField] private float speed = 2.0F;
    private Rigidbody rb;
    [SerializeField] private GameObject CameraObj;
    private float sensitivityX = 10;
    private float sensitivityY = 5;

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

        Vector3 move = new Vector3(x * speed * Time.deltaTime, 0f, z * speed * Time.deltaTime);
        move = transform.TransformDirection(move);
        rb.MovePosition(rb.position + move);
    }

    void Rotate(){
        float camJoyStickY = Input.GetAxis("Mouse Y");
        float camJoyStickX = Input.GetAxis("Mouse X");

        Quaternion camRotation = Quaternion.Euler(camJoyStickY * sensitivityY, 0, 0);
        Quaternion bodyRotation = Quaternion.Euler(0, camJoyStickX * sensitivityX, 0);

        CameraObj.transform.rotation *= camRotation;
        transform.rotation *= bodyRotation;

        float camX = CameraObj.transform.localEulerAngles.x;

        if (50f < camX && camX < 310f)
        {
            if (camX - 50f < 310f - camX)
            {
                camX = 50f;
            }
            else
            {
                camX = 310f;
            }
        }

        CameraObj.transform.localEulerAngles = new Vector3(camX, CameraObj.transform.localEulerAngles.y, 0);
    }
}
