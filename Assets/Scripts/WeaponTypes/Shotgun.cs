using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Shotgun")]
public class Shotgun : Weapons
{
    public override string weaponType { get; } = "Shotgun";
    public override string description { get; } = "initial bullet velocity: 100 m/s\nblast radius: 2m\nblast power: 8 x 10\nrate of fire: 75 rpm\nreload: 3 sec\nmagazine: 5";
    public override float shootPower { get; } = 100f;
    public override float shootCooldown { get; } = 0.8f;
    public override int magazineSize { get; } = 5;
    public override float reloadSpeed { get; } = 3f;
    private float numberBullets = 10;
    private float spread = 0.15f;

    public override string sfx_name { get; } = "event:/Shotgun Shoot";

    public override void ShootGun(Transform muzzle, Vector3 playerVelocity, int playerNum)
    {
        List<GameObject> bullets = new List<GameObject>();

        for (int i = 0; i < numberBullets; i++)
        {
            GameObject bulletInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);
            foreach (GameObject b in bullets)
            {
                Physics.IgnoreCollision(b.GetComponent<Collider>(), bulletInstance.GetComponent<Collider>());
            }
            bullets.Add(bulletInstance);

            BulletBehavior bulletBehavior = bulletInstance.GetComponent<BulletBehavior>();
            bulletBehavior.bullet = bulletType;
            bulletBehavior.playerNum = playerNum;
            bulletBehavior.bulletDirection = muzzle.forward;
            bulletBehavior.bulletSpeed = shootPower;

            Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();
            Vector3 shotDirection = muzzle.forward + muzzle.TransformDirection(new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0));
            bulletRB.velocity = (shotDirection * shootPower + playerVelocity / 2f) * bulletRB.mass;
        }
    }
}
