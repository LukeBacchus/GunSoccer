using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullets : ScriptableObject
{
    public abstract float bulletLifeTime { get; }
    public abstract float blastRadius { get; }
    public abstract float blastForce { get; }
    public abstract float selfBlastRadius { get; }
    public abstract float selfBlastForce { get; }

    public virtual void Movement(GameObject bullet, Vector3 bulletDirection, float bulletSpeed) { }
}
