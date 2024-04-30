using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpSystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerTagComponent, JumpEvent, GroundCheckSphereComponent, JumpComponent> _jumpFilter = null;

    public void Run()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        foreach (var i in _jumpFilter)
        {
            ref var entity = ref _jumpFilter.GetEntity(i);
            ref var groundCheck = ref _jumpFilter.Get3(i);
            ref var jumpComponent = ref _jumpFilter.Get4(i);
            ref var movable = ref entity.Get<MovableComponent>();
            ref var velocity = ref movable.velocity;

            if (!groundCheck.isGround) continue;

            velocity.y = Mathf.Sqrt(jumpComponent.force * -2f * movable.gravity);
        }
    }
}
