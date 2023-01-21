using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{

    private float sensitivityX = 10;
    private float sensitivityY = 5;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float camJoyStickY = Input.GetAxis("Mouse Y");
        float camJoyStickX = Input.GetAxis("Mouse X");

        Debug.Log(camJoyStickX);
        Debug.Log(camJoyStickY);


        Quaternion camRotation = Quaternion.Euler(camJoyStickY * sensitivityY, camJoyStickX * sensitivityX, 0);

        transform.rotation *= camRotation;

        float camX = transform.localEulerAngles.x;

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

        transform.localEulerAngles = new Vector3(camX, transform.localEulerAngles.y, 0);
    }
}
