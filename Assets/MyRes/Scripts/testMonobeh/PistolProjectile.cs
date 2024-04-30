using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolProjectile : Projectile
{
    protected override void OnTargetCollision(Collision collision, IDemageble demageble)
    {
        demageble.ApplyDamage(ProjectileDamage);
    }
}