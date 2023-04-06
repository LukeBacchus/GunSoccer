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
    public string description = "Gun description here";
    public abstract void ShootGun(Transform muzzle, Vector3 playerVelocity, int playerNum);

    public abstract string sfx_name { get; }
}
