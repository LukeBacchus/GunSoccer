using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullets/SMGBullet")]
public class SMGBullets : Bullets
{
    public override float bulletLifeTime { get; } = 5f;
    public override float blastRadius { get; } = 2f;
    public override float blastForce { get; } = 10f;
    public override float selfBlastRadius { get; } = 5f;
    public override float selfBlastForce { get; } = 20f;
}
