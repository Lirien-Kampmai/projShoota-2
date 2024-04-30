using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] private Transform _weaponMuzzle;
    [SerializeField] private Projectile _projPf;
    [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;
    [SerializeField] private float _force = 10f;

    public void PerformAttack()
    {
        var proj = Instantiate(_projPf, _weaponMuzzle.position, _weaponMuzzle.rotation);
        proj.ProjectileRigidbody.AddForce(_weaponMuzzle.forward * _force, _forceMode);
    }
}