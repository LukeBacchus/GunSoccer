using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullets/ShotgunBullet")]
public class ShotgunBullets : Bullets
{
    public override float bulletLifeTime { get; } = 2f;
    public override float blastRadius { get; } = 2f;
    public override float blastForce { get; } = 20f;
    public override float selfBlastRadius { get; } = 5f;
    public override float selfBlastForce { get; } = 25f;
}
