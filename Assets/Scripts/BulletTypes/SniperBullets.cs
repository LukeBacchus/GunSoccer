using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullets/SniperBullet")]
public class SniperBullets : Bullets
{
    public override float bulletLifeTime { get; } = 10f;
    public override float blastRadius { get; } = 1f;
    public override float blastForce { get; } = 80f;
    public override float selfBlastRadius { get; } = 7f;
    public override float selfBlastForce { get; } = 80f;
}
