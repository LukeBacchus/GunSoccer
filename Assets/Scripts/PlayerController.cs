using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerNum;
    [SerializeField] private float speed = 2.0F;
    private float maxSpeed = 5f;
    private Rigidbody rb;
    [SerializeField] private GameObject CameraObj;
    private float sensitivityX = 10;
    private float sensitivityY = 5;
    private float moveX;
    private float moveZ;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void MoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal" + (playerNum).ToString());
        float z = Input.GetAxisRaw("Vertical" + (playerNum).ToString());

        moveX = x;
        moveZ = z;
    }

    void Move()
    {
        Vector3 move = new Vector3(moveX * speed * Time.deltaTime, 0f, moveZ * speed * Time.deltaTime);

        rb.AddRelativeForce(move * 50, ForceMode.Impulse);
        float ymove = rb.velocity.y;
        rb.velocity = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0, rb.velocity.z), maxSpeed);
        rb.velocity = new Vector3(rb.velocity.x, ymove, rb.velocity.z);
    }

    void Rotate(){
        float camJoyStickY = Input.GetAxis("Mouse Y" + (playerNum).ToString());
        float camJoyStickX = Input.GetAxis("Mouse X" + (playerNum).ToString());

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
