using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseInputSystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerTagComponent, MouseLookDirectionComponent> _playerMouseInputFilter = null;

    private float axisX;
    private float axisY;


    public void Run()
    {
        GetAxis();
        ClampAxis();

        foreach (var i in _playerMouseInputFilter)
        {
            ref var lookDirectionComponent = ref _playerMouseInputFilter.Get2(i);

            lookDirectionComponent.directionMouse.X = axisX;
            lookDirectionComponent.directionMouse.Y = axisY;
        }
    }

    private void GetAxis()
    {
        axisX += Input.GetAxis("Mouse X");
        axisY -= Input.GetAxis("Mouse Y");
    }

    private void ClampAxis()
    {
        float x = axisX;
        float y = axisY;

        axisX = Mathf.Clamp(x, float.MinValue, float.MaxValue);
        axisY = Mathf.Clamp(y, float.MinValue, float.MaxValue);
    }
}
