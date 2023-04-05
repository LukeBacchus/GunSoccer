using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/GrenadeLauncher")]
public class GrenadeLauncher : Weapons
{
    public override string weaponType { get; } = "Grenade Launcher";
    public override float shootPower { get; } = 50f;
    public override float shootCooldown { get; } = 2f;
    public override int magazineSize { get; } = 1;
    public override float reloadSpeed { get; } = 2f;

    public override void ShootGun(Transform muzzle, int playerNum)
    {
        GameObject bulletInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);
        BulletBehavior bulletBehavior = bulletInstance.GetComponent<BulletBehavior>();
        bulletBehavior.bullet = bulletType;
        bulletBehavior.playerNum = playerNum;
        bulletBehavior.bulletDirection = muzzle.forward;
        bulletBehavior.bulletSpeed = shootPower;

        Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();
        bulletRB.velocity = muzzle.forward * shootPower * bulletRB.mass;
    }
}