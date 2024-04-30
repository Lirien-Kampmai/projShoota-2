using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [field: SerializeField] public int ProjectileDamage { get; private set; }
    [field: SerializeField] public Rigidbody ProjectileRigidbody { get; private set; }

    [field: SerializeField] public  ProjectileDisposeType _disposeType { get; private set; }

    [SerializeField] private ParticleSystem _effectOnDestroyPf;
    [SerializeField] private bool _isEffectOnDestroy = true;
    [field: SerializeField] private float _effectOnDestroyLifetime = 1f;

    private bool isProjectileDisposed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isProjectileDisposed) return;

        if (collision.gameObject.TryGetComponent(out IDemageble demageble))
        {
            OnTargetCollision(collision, demageble);

            if(_disposeType == ProjectileDisposeType.OnTargetCollision)
                ProjectileDisposed();
        }
        else
            OnOtherCollision(collision);

        OnAnyCollision(collision);

        if (_disposeType == ProjectileDisposeType.OnAnyCollision)
            ProjectileDisposed();
    }

    private void ProjectileDisposed()
    {
        OnProjectileDisposed();
        SpawnEffectDestroy();
        Destroy(gameObject);

        isProjectileDisposed = true;
    }

    private void SpawnEffectDestroy()
    {
        if (!_isEffectOnDestroy) return;

        var effect = Instantiate(_effectOnDestroyPf, transform.position, Quaternion.identity);
        Destroy(effect.gameObject, _effectOnDestroyLifetime);
    }

    protected virtual void OnProjectileDisposed() { }
    protected virtual void OnAnyCollision(Collision collision) { }

    protected virtual void OnOtherCollision(Collision collision) { }

    protected virtual void OnTargetCollision(Collision collision, IDemageble demageble) { }
}
