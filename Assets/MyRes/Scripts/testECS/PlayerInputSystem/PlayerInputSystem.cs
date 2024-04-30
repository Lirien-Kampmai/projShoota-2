using Leopotam.Ecs;
using System;
using UnityEngine;

class PlayerInputSystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerTagComponent, DirectionComponent> _playerInputFilter = null;

    private float moveX;
    private float moveZ;

    public void Run()
    {
        SetDirection();

        foreach(var i in _playerInputFilter)
        {
            ref var directionComponent = ref _playerInputFilter.Get2(i); // достаём DirectionComponent


            ref var direction = ref directionComponent.Direction;

            direction.X = moveX;
            direction.Z = moveZ;
        }
    }

    private void SetDirection()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
    }
}