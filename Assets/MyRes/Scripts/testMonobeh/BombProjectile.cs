using UnityEngine;

public class BombProjectile : Projectile
{
    [SerializeField] private Overlap _overlap;

    protected override void OnProjectileDisposed()
    {
        _overlap.PerformAttack(ProjectileDamage);
    }
}