using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookSystem : IEcsRunSystem, IEcsInitSystem
{
    private readonly EcsFilter<PlayerTagComponent> _playerFilter = null;
    private readonly EcsFilter<PlayerTagComponent, ModelComponent, MouseLookDirectionComponent> _playerLookFilter = null;

    private Quaternion startTransformRotation;

    public void Init()
    {
        foreach (var i in _playerFilter)
        {
            ref var entity = ref _playerFilter.GetEntity(i);
            ref var modelComponent = ref entity.Get<ModelComponent>();

            startTransformRotation = modelComponent.ModelTransform.rotation;
        }       
    }

    public void Run()
    {
        foreach (var i in _playerLookFilter)
        {
            ref var modelComponent = ref _playerLookFilter.Get2(i);
            ref var mouseLookDirectionComponent = ref _playerLookFilter.Get3(i);

            var axisX = mouseLookDirectionComponent.directionMouse.X;
            var axisY = mouseLookDirectionComponent.directionMouse.Y;

            var rotateX = Quaternion.AngleAxis(axisX, Vector3.up * Time.deltaTime * mouseLookDirectionComponent.mouseSensetiv);
            var rotateY = Quaternion.AngleAxis(axisY, Vector3.right * Time.deltaTime * mouseLookDirectionComponent.mouseSensetiv);

            modelComponent.ModelTransform.rotation = startTransformRotation * rotateX;
            mouseLookDirectionComponent.playerTransform.rotation = modelComponent.ModelTransform.rotation * rotateY;
        }
    }
}