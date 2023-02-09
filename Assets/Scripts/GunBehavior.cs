using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class GunBehavior : MonoBehaviour
{

    public GameObject bullet;
    public Transform muzzle;
    public Transform cam;
    private StudioEventEmitter sfx;
    [SerializeField] private int playerNum;

    private float currCooldown;
    private float shootCooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        currCooldown = 0;

        sfx = GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1" + (playerNum).ToString()) && currCooldown == 0)
        {
            ShootGun();
            currCooldown += shootCooldown;

            RuntimeManager.PlayOneShot("event:/Gunshot");

        }

        currCooldown = Mathf.Clamp(currCooldown - Time.deltaTime, 0, 10);
    }

    void ShootGun()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position + cam.forward * 2, cam.forward, out hit))
        {
            muzzle.LookAt(hit.point);
        } else
        {
            muzzle.localEulerAngles = new Vector3(0, 180, 0);
        }

        GameObject bulletInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);
        bulletInstance.GetComponent<BulletBehavior>().playerNum = playerNum;
        Physics.IgnoreCollision(bulletInstance.GetComponent<Collider>(), transform.GetComponentInParent<Collider>());
        
        Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();
        bulletRB.AddForce(muzzle.forward * 5000);
    }
}
