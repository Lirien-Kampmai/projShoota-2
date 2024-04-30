using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

public class ECSGameStart : MonoBehaviour
{
    private EcsWorld world; 
    private EcsSystems systems;

    private void Start()
    {
        world = new EcsWorld();
        systems = new EcsSystems(world);

        systems.ConvertScene();

        AddInjection();
        AddOneFrames();
        AddSystems();


        systems.Init();
    }

    private void AddInjection()
    {
        
    }

    private void AddOneFrames()
    {
        systems.OneFrame<JumpEvent>();
    }

    private void AddSystems()
    {
        systems.
            Add(new CursorLockSystem()).
            Add(new PlayerInputSystem()).
            Add(new MovementSystem()).
            Add(new PlayerMouseInputSystem()).
            Add(new PlayerLookSystem()).
            Add(new GroundCheckSystem()).
            Add(new GravitySystem()).
            
            Add(new PlayerJumpSystem());
    }


    private void Update()
    {
        systems.Run();
    }

    private void OnDestroy()
    {
        if(systems == null) return;

        systems.Destroy();
        systems=null;

        world.Destroy();
        world = null;
    }
}
