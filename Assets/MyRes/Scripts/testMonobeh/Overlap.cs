using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Overlap : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] private LayerMask _searchTargetLayerMask;
    [SerializeField] private Transform _overlapStartPoint;

    [Header("Offset")]
    [SerializeField] private Vector3 _offset;

    [Header("Overlap Area")]
    [SerializeField] private OverlapType _overlapType;
    [SerializeField] private Vector3 _boxSize = Vector3.one;
    [SerializeField] private float _sphereRadius = 1f;

    [Header("Obstacle")]
    [SerializeField] private bool _considerObstacle; // если тру то проверяем, есть ли перекрытие между стартовой поз. и найденым коллайдером какое-либо препятствие. Если есть то пропускаем цель при атаке.
    [SerializeField] private LayerMask _obstacleLayerMask; // слой, через котоорый при поиске не будет проходить урон.

    [Header("Gizmos")]
    [SerializeField] private DrawGizmosType _drawGizmosType;
    [SerializeField] private Color _gizmosColor = Color.white;

    private readonly Collider[] _overlapsResults = new Collider[32];
    private int _overlapsResultsCount;

    public void PerformAttack(int damage)
    {
        if (TryFindEnemy())
            TryAttackEnemy(damage);
    }

    private void TryAttackEnemy(int damage)
    {
        for (int i = 0; i < _overlapsResultsCount; i++)
        {
            if (_overlapsResults[i].TryGetComponent(out IDemageble demageble) == false)
            { continue; }

            if (_considerObstacle)
            {
                var startPointPos = _overlapStartPoint.position;
                var colliderPos = _overlapsResults[i].transform.position;
                var hasObstacle = Physics.Linecast(startPointPos, colliderPos, _obstacleLayerMask.value);

                if (hasObstacle)
                { continue; }
            }
            demageble.ApplyDamage(damage);
        }
    }

    private bool TryFindEnemy()
    {
        var pos = _overlapStartPoint.TransformPoint(_offset);

        _overlapsResultsCount = _overlapType switch
        {
            OverlapType.Box => OverlapBox(pos),
            OverlapType.Sphere => OverlapSphere(pos),

            _ => throw new ArgumentOutOfRangeException(nameof(_overlapType))
        }; 

        return _overlapsResultsCount > 0;
    }

    private int OverlapBox(Vector3 pos)
    {
        return Physics.OverlapBoxNonAlloc(pos, _boxSize / 2, _overlapsResults, _overlapStartPoint.rotation, _searchTargetLayerMask.value);
    }

    private int OverlapSphere(Vector3 pos)
    {
        return Physics.OverlapSphereNonAlloc(pos, _sphereRadius, _overlapsResults, _searchTargetLayerMask.value);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        TryDrawGizmos(DrawGizmosType.Always);
    }
    private void OnDrawGizmosSelected()
    {
        TryDrawGizmos(DrawGizmosType.OnSelected);
    }

    private void TryDrawGizmos(DrawGizmosType type)
    {
        if (_drawGizmosType != type) return;
        if (_overlapStartPoint == null) return;

        Gizmos.matrix = _overlapStartPoint.localToWorldMatrix;
        Gizmos.color = _gizmosColor;

        switch(_overlapType)
        {
            case OverlapType.Box: Gizmos.DrawCube(_offset, _boxSize); break;
            case OverlapType.Sphere: Gizmos.DrawSphere(_offset, _sphereRadius); break;

            default: throw new ArgumentOutOfRangeException(nameof(_overlapType));
        }; 
    }
#endif
}