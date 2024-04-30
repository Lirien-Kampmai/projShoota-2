using System;
using System.Numerics;
using UnityEngine;

[Serializable]
public struct MouseLookDirectionComponent
{
    public System.Numerics.Vector2 directionMouse;

    public Transform playerTransform;
    [Range(0, 10)]public float mouseSensetiv;
}