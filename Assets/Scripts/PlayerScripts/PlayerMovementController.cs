using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveX;
    public float moveZ;
    public float sprint;
    public float camJoyStickX = 0;
    public float camJoyStickY = 0;
    public bool jump = false;
    public bool targetLocked = false;

    private PlayerStats playerStats;
    private Rigidbody rb;
    private Collider col;
    [SerializeField] public float jumpForce = 50;
    [SerializeField] private float lookMin = 60f;
    [SerializeField] private float lookMax = 310f;
    [SerializeField] private float speed = 500;
    [SerializeField] private float sprintSpeed = 500;
    private float maxSpeed = 10;
    private float maxUpSpeed = 15;
    private float maxFallSpeed = 20;
    private float maxSlope = 60;
    private Vector3 slopeNormal = new Vector3(0, 1, 0);


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

        Move();
        if (jump)
        {
            Jump();
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
        if (playerStats.allowPlayerMovement)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddRelativeForce(new Vector3(0, jumpForce * rb.mass, 0), ForceMode.Impulse);
        }
        jump = false;
    }

    void Move()
    {
        if (playerStats.allowPlayerMovement)
        {
            Vector3 step = transform.TransformDirection(new Vector3(moveX, 0, moveZ));
            if (OnSlope() && IsGrounded())
            {
                step = Vector3.ProjectOnPlane(step, slopeNormal);
            }
            Vector3 move = step.normalized * rb.mass * (speed + sprint * sprintSpeed);
            rb.AddForce(move, ForceMode.Force);

            LimitSpeed();
        }
    }

    void Rotate(){
        if (playerStats.allowPlayerRotate)
        {
            if (!targetLocked)
            {
                Quaternion camRotation = Quaternion.Euler(playerStats.rotationSpeed * camJoyStickY * playerStats.sensitivityY * Time.deltaTime, 0, 0);
                Quaternion bodyRotation = Quaternion.Euler(0, playerStats.rotationSpeed * camJoyStickX * playerStats.sensitivityX * Time.deltaTime, 0);

                playerStats.cam.transform.rotation *= camRotation;
                transform.rotation *= bodyRotation;
            }
            else if (playerStats.soccerBallBehavior != null)
            {
                Vector3 targetPosition = playerStats.soccerBallBehavior.GetPosition() - playerStats.cam.transform.position;
                Vector3 lookTargetY = new Vector3(0, targetPosition.y, Mathf.Sqrt(Mathf.Pow(targetPosition.z, 2) + Mathf.Pow(targetPosition.x, 2)));
                Vector3 lookTargetX = new Vector3(targetPosition.x, 0, targetPosition.z);
                Quaternion targetRotationY = Quaternion.LookRotation(lookTargetY);
                Quaternion targetRotationX = Quaternion.LookRotation(lookTargetX);
                float angleY = Vector3.Angle(playerStats.cam.transform.forward, lookTargetY);
                float angleX = Vector3.Angle(transform.forward, lookTargetX);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotationX, 360 * Time.deltaTime / angleX);
                playerStats.cam.transform.rotation = Quaternion.Lerp(playerStats.cam.transform.rotation, targetRotationY, 360 * Time.deltaTime / angleY);
            }
            else
            {
                targetLocked = false;
            }

            float camX = playerStats.cam.transform.localEulerAngles.x;

            if (lookMin < camX && camX < lookMax)
            {
                if (camX - lookMin < lookMax - camX)
                {
                    camX = lookMin;
                }
                else
                {
                    camX = lookMax;
                }
            }

            playerStats.cam.transform.localEulerAngles = new Vector3(camX, 0, 0);
        }
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

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Stadium Surface"){
            if (playerStats.team == "Blue"){
                playerStats.BlueJetLeft.SetActive(false);
                playerStats.BlueJetRight.SetActive(false);
            } else {
                playerStats.RedJetLeft.SetActive(false);
                playerStats.RedJetRight.SetActive(false);
            }
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "Stadium Surface"){
            if (playerStats.team == "Blue"){
                playerStats.BlueJetLeft.SetActive(true);
                playerStats.BlueJetRight.SetActive(true);
            } else {
                playerStats.RedJetLeft.SetActive(true);
                playerStats.RedJetRight.SetActive(true);
            }
        }
    }

}
