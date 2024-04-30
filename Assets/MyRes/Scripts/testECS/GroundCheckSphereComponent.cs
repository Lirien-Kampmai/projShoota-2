using System;
using UnityEngine;

[Serializable]
public struct GroundCheckSphereComponent
{
    public LayerMask ground;
    public Transform groundCheckSpherePosition;
    public float radiusCheckSphere;
    public bool isGround;
}
