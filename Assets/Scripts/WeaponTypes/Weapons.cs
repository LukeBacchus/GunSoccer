using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : ScriptableObject
{
    public abstract string weaponType { get; }
    public abstract float shootPower { get; }
    public abstract float shootCooldown { get; }
    public abstract int magazineSize { get; }
    public abstract float reloadSpeed { get; }

    public GameObject gunModel;
    public GameObject bullet;
    public Bullets bulletType;
    public Sprite icon;

    public abstract void ShootGun(Transform muzzle, int playerNum);
}
