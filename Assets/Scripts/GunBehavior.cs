using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{

    public GameObject bullet;
    public Transform muzzle;

    private float currCooldown;
    private float shootCooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        currCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && currCooldown == 0)
        {
            ShootGun();
            currCooldown += shootCooldown;
        }

        currCooldown = Mathf.Clamp(currCooldown - Time.deltaTime, 0, 10);
    }

    void ShootGun()
    {
        GameObject bulletInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);
        Physics.IgnoreCollision(bulletInstance.GetComponent<Collider>(), transform.GetComponentInParent<Collider>());
        
        Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();
        bulletRB.AddForce(muzzle.forward * 1000);
    }
}
