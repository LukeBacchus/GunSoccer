using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private PlayerMovementController playerController;
    private PlayerGunController playerGunController;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        playerController = GetComponent<PlayerMovementController>();
        playerGunController = GetComponent<PlayerGunController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.allowPlayerInput)
        {
            PlayerInput();
        }
        else
        {
            RotateInput();
        }
    }

    void PlayerInput()
    {
        JumpInput();
        MoveInput();
        RotateInput();
        ShootInput();
    }

    void JumpInput()
    {
        if (Input.GetButtonDown("Jump" + (playerStats.playerNum).ToString()) && playerController.IsGrounded())
        {
            playerController.jump = true;
        }
    }

    void MoveInput()
    {
        float x = Input.GetAxisRaw("X joy" + (playerStats.playerNum).ToString());
        float z = Input.GetAxisRaw("Y joy" + (playerStats.playerNum).ToString());
        // float x = Input.GetAxisRaw("Horizontal" + (playerStats.playerNum).ToString());
        // float z = Input.GetAxisRaw("Vertical" + (playerStats.playerNum).ToString());

        playerController.moveX = x;
        playerController.moveZ = z;
    }

    void RotateInput()
    {
        // Gyro
        playerController.camGyroY = Input.GetAxis("Mouse Y" + (playerStats.playerNum).ToString());
        playerController.camGyroX = Input.GetAxis("Mouse X" + (playerStats.playerNum).ToString());

        // Joystick
        playerController.camJoyStickX = Input.GetAxisRaw("Horizontal" + (playerStats.playerNum).ToString());
        playerController.camJoyStickY = Input.GetAxisRaw("Vertical" + (playerStats.playerNum).ToString());
        // playerController.camJoyStickY = Input.GetAxis("Y joy" + (playerStats.playerNum).ToString());
        // playerController.camJoyStickX = Input.GetAxis("X joy" + (playerStats.playerNum).ToString());
    }

    void ShootInput()
    {
        if (Input.GetButton("Fire1" + (playerStats.playerNum).ToString()))
        {
            playerGunController.shoot = true;
        }
    }
}
