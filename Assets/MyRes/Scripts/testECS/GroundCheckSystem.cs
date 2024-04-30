using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckSystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerTagComponent, GroundCheckSphereComponent> _checkFilter = null;

    public void Run()
    {
        foreach (var i in _checkFilter)
        {
            ref var groundCheck = ref _checkFilter.Get2(i);

            groundCheck.isGround = Physics.CheckSphere(groundCheck.groundCheckSpherePosition.position, groundCheck.radiusCheckSphere, groundCheck.ground);
        }
    }
}
