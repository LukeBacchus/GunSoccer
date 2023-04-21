using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/AssaultRifle")]
public class AssaultRifle : Weapons
{
    public override string weaponType { get; } = "Assault Rifle";
    public override string description { get; } = "initial bullet velocity: 180 m/s\nblast radius: 1.5m\nblast power: 20\nrate of fire: 200 rpm\nreload: 1.5 sec\nmagazine: 30";
    public override float shootPower { get; } = 180f;
    public override float shootCooldown { get; } = 0.3f;
    public override int magazineSize { get; } = 30;
    public override float reloadSpeed { get; } = 1.5f;

    public override string sfx_name { get; } = "event:/Rifle Shoot";

    public override void ShootGun(Transform muzzle, Vector3 playerVelocity, int playerNum)
    {
        GameObject bulletInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);
        BulletBehavior bulletBehavior = bulletInstance.GetComponent<BulletBehavior>();
        bulletBehavior.bullet = bulletType;
        bulletBehavior.playerNum = playerNum;
        bulletBehavior.bulletDirection = muzzle.forward;
        bulletBehavior.bulletSpeed = shootPower;
        
        Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();
        bulletRB.velocity = (muzzle.forward * shootPower + playerVelocity / 2f) * bulletRB.mass;
    }
}
