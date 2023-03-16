using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerGunController : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform cam;
    public bool shoot;

    private StudioEventEmitter sfx;
    private PlayerStats playerStats;

    private float currCooldown;
    private int currMagazine;
    private bool reloading;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<StudioEventEmitter>();
        playerStats = GetComponent<PlayerStats>();

        currCooldown = 0;
        currMagazine = playerStats.weapon.magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (currMagazine == 0)
        {
            reloading = true;
            StartCoroutine(Reload());
        }

        if (shoot)
        {
            if (currCooldown == 0 && !reloading && playerStats.allowPlayerShoot)
            {
                ShootGun();
                currCooldown += playerStats.weapon.shootCooldown;

                RuntimeManager.PlayOneShot(playerStats.weapon.sfx_name);

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

        playerStats.weapon.ShootGun(muzzle, GetComponent<Rigidbody>().velocity, playerStats.playerNum);
        currMagazine -= 1;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(playerStats.weapon.reloadSpeed);
        currMagazine = playerStats.weapon.magazineSize;
        reloading = false;
    }
}
