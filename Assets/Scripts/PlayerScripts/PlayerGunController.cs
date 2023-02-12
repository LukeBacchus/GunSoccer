using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerGunController : MonoBehaviour
{
    [SerializeField] private Weapons weapon;
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
        currCooldown = 0;
        currMagazine = weapon.magazineSize;

        sfx = GetComponent<StudioEventEmitter>();
        playerStats = GetComponent<PlayerStats>();
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
            if (currCooldown == 0 && !reloading)
            {
                ShootGun();
                currCooldown += weapon.shootCooldown;

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

        weapon.ShootGun(muzzle, playerStats.playerNum);
        currMagazine -= 1;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(weapon.reloadSpeed);
        currMagazine = weapon.magazineSize;
        reloading = false;
    }
}
