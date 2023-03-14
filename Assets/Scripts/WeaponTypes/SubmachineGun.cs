using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/SubmachineGun")]
public class SubmachineGun : Weapons
{
    public override string weaponType { get; } = "Submachine Gun";
    public override float shootPower { get; } = 120f;
    public override float shootCooldown { get; } = 0.1f;
    public override int magazineSize { get; } = 30;
    public override float reloadSpeed { get; } = 0.8f;
    private float randomAccuracy = 0.05f;

    public override void ShootGun(Transform muzzle, Vector3 playerVelocity, int playerNum)
    {
        GameObject bulletInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);
        BulletBehavior bulletBehavior = bulletInstance.GetComponent<BulletBehavior>();
        bulletBehavior.bullet = bulletType;
        bulletBehavior.playerNum = playerNum;
        bulletBehavior.bulletDirection = muzzle.forward;
        bulletBehavior.bulletSpeed = shootPower;

        Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();
        Vector3 shotDirection = muzzle.forward + muzzle.TransformDirection(new Vector3(Random.Range(-randomAccuracy, randomAccuracy), Random.Range(-randomAccuracy, randomAccuracy), 0));
        bulletRB.velocity = (shotDirection * shootPower + playerVelocity / 2f) * bulletRB.mass;
    }
}
