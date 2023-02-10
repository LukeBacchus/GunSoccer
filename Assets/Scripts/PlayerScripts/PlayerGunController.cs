using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerGunController : MonoBehaviour
{
    public GameObject bullet;
    public Transform muzzle;
    public Transform cam;
    public bool shoot;

    private StudioEventEmitter sfx;
    private PlayerStats playerStats;

    private float currCooldown;
    private float shootCooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        currCooldown = 0;

        sfx = GetComponent<StudioEventEmitter>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            if (currCooldown == 0)
            {
                ShootGun();
                currCooldown += shootCooldown;

                RuntimeManager.PlayOneShot("event:/Gunshot");
            }

            shoot = false;
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
        bulletInstance.GetComponent<BulletBehavior>().playerNum = playerStats.playerNum;
        
        Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();
        bulletRB.AddForce(muzzle.forward * 5000);
    }
}
