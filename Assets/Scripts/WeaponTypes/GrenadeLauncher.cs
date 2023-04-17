using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/GrenadeLauncher")]
public class GrenadeLauncher : Weapons
{
    public override string weaponType { get; } = "Grenade Launcher";
    public override string description { get; } = "initial bullet velocity: 50 m/s\nblast radius: 20m\nblast power: 100\nrate of fire: 24 rpm\nreload: 2.5 sec\nmagazine: 1";
    public override float shootPower { get; } = 50f;
    public override float shootCooldown { get; } = 2.5f;
    public override int magazineSize { get; } = 1;
    public override float reloadSpeed { get; } = 2.5f;

    public override string sfx_name { get; } = "event:/Grenade Launcher Shoot";

    public override void ShootGun(Transform muzzle, Vector3 playerVelocity, int playerNum)
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