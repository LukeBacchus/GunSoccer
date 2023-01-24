using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerNum;
    private Rigidbody rb;
    [SerializeField] private GameObject CameraObj;
    private Collider col;
    private float speed = 50;
    private float maxSpeed = 10;
    private float maxUpSpeed = 15;
    private float maxFallSpeed = 20;
    private float maxSlope = 60;
    private float sensitivityX = 10;
    private float sensitivityY = 5;
    private float moveX;
    private float moveZ;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        col = GetComponent<CapsuleCollider>();
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
        RaycastHit hit;
        Physics.Raycast(col.bounds.center, -transform.up, out hit, col.bounds.size.y);
        float currSlope = Vector3.Angle(Vector3.up, hit.normal);

        Vector3 step = transform.TransformDirection(new Vector3(moveX, 0, moveZ));
        if (currSlope <= maxSlope && currSlope != 0)
        {
            step = Vector3.ProjectOnPlane(step, hit.normal);
        }
        Vector3 move = step.normalized * rb.mass * speed * Time.fixedDeltaTime;
        rb.AddForce(move, ForceMode.Impulse);

        float ymove = Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxUpSpeed);
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
