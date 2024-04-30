using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GravitySystem : IEcsRunSystem
{
    private readonly EcsFilter<PlayerTagComponent, MovableComponent> _gravityFilter = null;
    public void Run()
    {
        foreach (var i in _gravityFilter)
        {
            ref var movableComponent = ref _gravityFilter.Get2(i);
            ref var velocity = ref movableComponent.velocity;

            ref var personController = ref movableComponent.CharacterController;

            velocity.y += movableComponent.gravity * Time.deltaTime;
            personController.Move(velocity * Time.deltaTime);
        }
    }
}
