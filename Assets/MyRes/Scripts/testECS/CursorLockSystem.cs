using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLockSystem : IEcsInitSystem
{
    public void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}