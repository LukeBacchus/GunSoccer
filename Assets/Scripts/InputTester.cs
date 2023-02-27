using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump1")){
            Debug.Log("Jump1");
        }

        if(Input.GetButtonDown("Jump2")){
            Debug.Log("Jump2");
        }

        if(Input.GetButtonDown("Jump3")){
            Debug.Log("Jump3");
        }

        if(Input.GetButtonDown("Jump4")){
            Debug.Log("Jump4");
        }

        if(Input.GetButtonDown("Fire11")){
            Debug.Log("Fire11");
        }
        
        if(Input.GetButtonDown("Fire12")){
            Debug.Log("Fire12");
        }
        
        if(Input.GetButtonDown("Fire13")){
            Debug.Log("Fire13");
        }

        if(Input.GetButtonDown("Fire14")){
            Debug.Log("Fire14");
        }

        // float h1 = Input.GetAxisRaw("Horizontal1");
        // float v1 = Input.GetAxisRaw("Vertical1");
        // float h2 = Input.GetAxisRaw("Horizontal2");
        // float v2 = Input.GetAxisRaw("Vertical2");
        // float mx1 = Input.GetAxisRaw("Mouse X1");
        // float my1 = Input.GetAxisRaw("Mouse Y1");
        // float mx2 = Input.GetAxisRaw("Mouse X2");
        // float my2 = Input.GetAxisRaw("Mouse Y2");
        // float sw = Input.GetAxisRaw("Mouse ScrollWheel");

        // Debug.Log("h1: " + h1);
        // Debug.Log("v1: " + v1);
        // Debug.Log("h2: " + h2);
        // Debug.Log("v2: " + v2);
        // Debug.Log("mx1: " + mx1);
        // Debug.Log("my1: " + my1);
        // Debug.Log("mx2: " + mx2);
        // Debug.Log("my2: " + my2);
        // Debug.Log("sw: " + sw);

        // Debug.Log(Input.GetAxis("X test").ToString("F5"));
        // Debug.Log("X test: " + Input.GetAxis("X test").ToString("F5"));
        // Debug.Log("Y test: " + Input.GetAxis("Y test").ToString("F5"));
        // Debug.Log("X test 2: " + Input.GetAxis("X test 2").ToString("F5"));
        // Debug.Log("Y test 2: " + Input.GetAxis("Y test 2").ToString("F5"));
        // Debug.Log("3 test: " + Input.GetAxisRaw("3 test").ToString("F5"));
        // Debug.Log("4 test: " + Input.GetAxisRaw("4 test").ToString("F5"));
        // Debug.Log("5 test: " + Input.GetAxisRaw("5 test").ToString("F5"));
        // Debug.Log("6 test: " + Input.GetAxisRaw("6 test").ToString("F5"));
        // Debug.Log("7 test: " + Input.GetAxisRaw("7 test").ToString("F5"));
        // Debug.Log("8 test: " + Input.GetAxisRaw("8 test").ToString("F5"));
        // Debug.Log("9 test: " + Input.GetAxisRaw("9 test").ToString("F5"));
        // Debug.Log("10 test: " + Input.GetAxisRaw("10 test").ToString("F5"));

    }
}
