using Leopotam.Ecs;
using UnityEngine;

sealed class MovementSystem : IEcsRunSystem
{
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<MovableComponent, DirectionComponent, ModelComponent> _moveFilter = null; // указываются компоненты, которые должны быть на компоненте при поиске


    public void Run()
    {
        foreach (var i in _moveFilter)
        {
            ref var movementComponent = ref _moveFilter.Get1(i);
            ref var directionComponent = ref _moveFilter.Get2(i);
            ref var modelComponent = ref _moveFilter.Get3(i);

            ref var personController = ref movementComponent.CharacterController;
            ref var speed = ref movementComponent.SpeedPerson;
            ref var direction = ref directionComponent.Direction;
            ref var transform = ref modelComponent.ModelTransform;
            ref var velocity = ref movementComponent.velocity;

            var rawDir = (transform.right * direction.X) + (transform.forward * direction.Z);

            personController.Move(rawDir * speed * Time.deltaTime);
        }
    }
}