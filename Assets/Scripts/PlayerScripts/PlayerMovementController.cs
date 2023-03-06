using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveX;
    public float moveZ;
    public float camJoyStickX = 0;
    public float camJoyStickY = 0;
    public bool jump = false;

    private PlayerStats playerStats;
    private Rigidbody rb;
    private Collider col;
    private float jumpForce = 10;
    [SerializeField] private float speed = 50;
    private float maxSpeed = 10;
    private float maxUpSpeed = 15;
    private float maxFallSpeed = 20;
    private float maxSlope = 60;
    private Vector3 slopeNormal = new Vector3(0, 1, 0);
    private float sensitivityX = 10;
    private float sensitivityY = 5;
    [SerializeField] private float rotationSpeed = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        ApplyGravity();

        if (playerStats.allowPlayerMovement)
        {
            Move();
            if (jump)
            {
                Jump();
            }
        }
    }

    public bool IsGrounded()
    {
        if (Physics.Raycast(col.bounds.center + col.bounds.extents.z * transform.forward, -transform.up, out _, col.bounds.size.y * 0.5f + 0.05f) ||
            Physics.Raycast(col.bounds.center + col.bounds.extents.z * -transform.forward, -transform.up, out _, col.bounds.size.y * 0.5f + 0.05f) ||
            Physics.Raycast(col.bounds.center + col.bounds.extents.x * transform.right, -transform.up, out _, col.bounds.size.y * 0.5f + 0.05f) ||
            Physics.Raycast(col.bounds.center + col.bounds.extents.x * -transform.right, -transform.up, out _, col.bounds.size.y * 0.5f + 0.05f) ||
            Physics.Raycast(col.bounds.center, -transform.up, out _, col.bounds.size.y * 0.5f + 0.1f))
        {
            return true;
        }
        return false;
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddRelativeForce(new Vector3(0, jumpForce * rb.mass, 0), ForceMode.Impulse);
        jump = false;
    }

    void Move()
    {
        Vector3 step = transform.TransformDirection(new Vector3(moveX, 0, moveZ));
        if (OnSlope() && IsGrounded())
        {
            step = Vector3.ProjectOnPlane(step, slopeNormal);
        }
        Vector3 move = step.normalized * rb.mass * speed;
        rb.AddForce(move, ForceMode.Force);

        LimitSpeed();
    }

    void Rotate(){
        Quaternion camRotation = Quaternion.Euler(rotationSpeed * camJoyStickY * sensitivityY, 0, 0);
        Quaternion bodyRotation = Quaternion.Euler(0, rotationSpeed * camJoyStickX * sensitivityX, 0);

        playerStats.cam.transform.rotation *= camRotation;
        transform.rotation *= bodyRotation;

        float camX = playerStats.cam.transform.localEulerAngles.x;

        if (60f < camX && camX < 310f)
        {
            if (camX - 60f < 310f - camX)
            {
                camX = 60f;
            }
            else
            {
                camX = 310f;
            }
        }

        playerStats.cam.transform.localEulerAngles = new Vector3(camX, 0, 0);
    }

    void ApplyGravity()
    {
        if (IsGrounded() && OnSlope())
        {
            rb.useGravity = false;
            rb.AddForce(-slopeNormal * Physics.gravity.magnitude * rb.mass, ForceMode.Force);
        } else
        {
            rb.useGravity = true;
        }
    }

    private void LimitSpeed()
    {
        if (OnSlope() && IsGrounded())
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        else
        {
            float ymove = Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxUpSpeed);
            rb.velocity = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0, rb.velocity.z), maxSpeed);
            rb.velocity = new Vector3(rb.velocity.x, ymove, rb.velocity.z);
        }
    }

    private bool OnSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(col.bounds.center, -transform.up, out hit, col.bounds.size.y))
        {
            slopeNormal = hit.normal;
            float currSlope = Vector3.Angle(Vector3.up, slopeNormal);
            return currSlope <= maxSlope && currSlope != 0;
        }
        return false;
    }
}
