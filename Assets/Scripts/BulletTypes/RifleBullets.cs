using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullets/RifleBullet")]
public class RifleBullets : Bullets
{
    public override float bulletLifeTime { get; } = 10f;
    public override float blastRadius { get; } = 3f;
    public override float blastForce { get; } = 20f;
    public override float selfBlastRadius { get; } = 7f;
    public override float selfBlastForce { get; } = 30f;
}
