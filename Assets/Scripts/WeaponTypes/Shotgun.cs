using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Shotgun")]
public class Shotgun : Weapons
{
    public override string weaponType { get; } = "Shotgun";
    public override float shootPower { get; } = 100f;
    public override float shootCooldown { get; } = 0.8f;
    public override int magazineSize { get; } = 5;
    public override float reloadSpeed { get; } = 1.5f;
    private float numberBullets = 10;
    private float spread = 0.1f;

    public override void ShootGun(Transform muzzle, Vector3 playerVelocity, int playerNum)
    {
        for (int i = 0; i < numberBullets; i++)
        {
            GameObject bulletInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);
            BulletBehavior bulletBehavior = bulletInstance.GetComponent<BulletBehavior>();
            bulletBehavior.bullet = bulletType;
            bulletBehavior.playerNum = playerNum;
            bulletBehavior.bulletDirection = muzzle.forward;
            bulletBehavior.bulletSpeed = shootPower;

            Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();
            Vector3 shotDirection = muzzle.forward + muzzle.TransformDirection(new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0));
            bulletRB.velocity = (shotDirection * shootPower + playerVelocity) * bulletRB.mass;
        }
    }
}
