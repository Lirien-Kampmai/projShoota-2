using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaycastAttack : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField, Min(0f)] private int _damage = 10;

    [Header("Raycast")]
    [SerializeField] private Transform _pointCreateRay;
    [SerializeField] private LayerMask _layerMask; // layers for attack(ignor other)
    [SerializeField, Min(0f)] private float _distance = 1000f;
    [SerializeField, Min(0f)] private int _shotCount = 1;

    [Header("Spread")]
    [SerializeField] private bool _isUseSpread;
    [SerializeField, Min(0f)] private float _spreadFactor;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MakeAttack();
    }

    public void MakeAttack()
    {
        for (int i = 0; i < _shotCount; i++)
            MakeRaycast();
    }

    private void MakeRaycast()
    {
        Vector3 dir = _isUseSpread ? _pointCreateRay.forward + CalculateSpread() : _pointCreateRay.forward;

        Ray ray = new Ray(_pointCreateRay.position, dir) ;

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _layerMask))
        {
            var hitCollider = hitInfo.collider;

            if (hitCollider.TryGetComponent(out IDemageble demageble))
                demageble.ApplyDamage(_damage);
            else
                Debug.Log("IDemageble is not found");
        }
    }

    private Vector3 CalculateSpread()
    {
        return new Vector3
        {
            x = Random.Range(-_spreadFactor, _spreadFactor),
            y = Random.Range(-_spreadFactor, _spreadFactor),
            z = Random.Range(-_spreadFactor, _spreadFactor)
        };
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var ray = new Ray(_pointCreateRay.position, _pointCreateRay.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _layerMask))
        {
            DrawRay(ray, hitInfo.point, hitInfo.distance, Color.red);
        }
        else
        {
            var hitPos = ray.origin + ray.direction * _distance;
            DrawRay(ray, hitPos, _distance, Color.green);
        }

    }

    private void DrawRay(Ray ray, Vector3 hitPos, float distance, Color color)
    {
        const float hitPointRad = 0.15f;

        Debug.DrawRay(ray.origin, ray.direction * distance, color);

        Gizmos.color = color;
        Gizmos.DrawSphere(hitPos, hitPointRad);
    }
#endif
}
