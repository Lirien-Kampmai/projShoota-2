using System;
using Voody.UniLeo;
using UnityEngine;

[Serializable]
public struct MovableComponent
{
    public CharacterController CharacterController;
    public float SpeedPerson;
    public Vector3 velocity;
    public float gravity;
}