using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/SniperRifle")]
public class SniperRifle : Weapons
{
    public override string weaponType { get; } = "Sniper Rifle";
    public override float shootPower { get; } = 250f;
    public override float shootCooldown { get; } = 1f;
    public override int magazineSize { get; } = 5;
    public override float reloadSpeed { get; } = 4f;

    public override string sfx_name { get; } = "event:/Sniper";

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
