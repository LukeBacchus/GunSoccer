using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private bool initializedMagazineText = false;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<StudioEventEmitter>();
        playerStats = GetComponent<PlayerStats>();
        muzzle = playerStats.gunPosSee.GetChild(0).Find("Muzzle").transform;

        currCooldown = 0;
        currMagazine = playerStats.weapon.magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.gameState != null && !initializedMagazineText)
        {
            UpdateMagazineText();
            initializedMagazineText = true;
        }

        if (currMagazine == 0 && !reloading)
        {
            reloading = true;
            StartCoroutine(Reload());
        }

        if (Input.GetButtonDown("reload" + playerStats.playerNum) && !reloading && currMagazine != playerStats.weapon.magazineSize)
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

    public void UpdateMuzzleLocation()
    {
        muzzle = playerStats.gunPosSee.GetChild(0).Find("Muzzle").transform;
    }

    private void ShootGun()
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
        UpdateMagazineText();
    }

    private void UpdateMagazineText()
    {
        if (playerStats.gameState != null)
        {
            playerStats.gameState.playerMagazineTexts[playerStats.playerNum - 1].text = currMagazine + "/" + playerStats.weapon.magazineSize;
        }
    }

    IEnumerator Reload()
    {
        if (playerStats.gameState == null)
        {
            yield break;
        }

        Transform crosshair = playerStats.gameState.playerCrosshairs[playerStats.playerNum - 1];
        Image reloadImage = crosshair.transform.GetChild(0).GetComponent<Image>();
        crosshair.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        float count = 0;
        while (count <= playerStats.weapon.reloadSpeed)
        {
            count += Time.deltaTime;
            reloadImage.fillAmount = count / playerStats.weapon.reloadSpeed;
            yield return null;
        }

        currMagazine = playerStats.weapon.magazineSize;
        reloadImage.fillAmount = 0;
        crosshair.GetComponent<Image>().color = new Color(1, 1, 1);
        UpdateMagazineText();
        reloading = false;
    }
}
