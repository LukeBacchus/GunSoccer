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
        PlayerInput();
        RotateInput();
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
        float x = Input.GetAxisRaw("Horizontal" + (playerStats.playerNum).ToString());
        float z = Input.GetAxisRaw("Vertical" + (playerStats.playerNum).ToString());

        playerController.moveX = x;
        playerController.moveZ = z;
    }

    void RotateInput()
    {
        playerController.camJoyStickY = Input.GetAxis("Mouse Y" + (playerStats.playerNum).ToString());
        playerController.camJoyStickX = Input.GetAxis("Mouse X" + (playerStats.playerNum).ToString());
    }

    void ShootInput()
    {
        if (Input.GetButton("Fire1" + (playerStats.playerNum).ToString()))
        {
            playerGunController.shoot = true;
        }
    }
}
