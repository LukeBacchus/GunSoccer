using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullets/GrenadeLauncherBullet")]
public class GrenadeLauncherBullets : Bullets
{
    public override float bulletLifeTime { get; } = 20f;
    public override float blastRadius { get; } = 15f;
    public override float blastForce { get; } = 100f;
    public override float selfBlastRadius { get; } = 15f;
    public override float selfBlastForce { get; } = 100f;
    public override void Movement(GameObject bullet, Vector3 bulletDirection, float bulletSpeed)
    {
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletDirection * bulletSpeed * rb.mass;
    }
}
