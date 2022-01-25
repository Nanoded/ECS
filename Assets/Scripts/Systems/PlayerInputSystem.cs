using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : SystemBase
{
    private InputAction _inputAction;
    private float2 _movement;

    private InputAction _shootAction;
    private float _shootInput;

    private float2 _direction;


    protected override void OnStartRunning()
    {
        _inputAction = new InputAction("Move", binding: ("<Gamepad>/rightStick"));
        _inputAction.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Right", "<Keyboard>/d")
            .With("Left", "<Keyboard>/a");

        _inputAction.performed += context => { _movement = context.ReadValue<Vector2>(); };
        _inputAction.started += context => { _movement = context.ReadValue<Vector2>(); };
        _inputAction.canceled += context => { _movement = context.ReadValue<Vector2>(); };

        _inputAction.Enable();

        _shootAction = new InputAction("Shoot", binding: ("<Keyboard>/F"));
        _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };

        _shootAction.Enable();

        _direction = new float2(0, 1);
    }

    protected override void OnStopRunning()
    {
        _inputAction.Disable();
        _shootAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.ForEach(
            (ref MovementComponent movementComponent, ref JerkComponent jerkComponent, in Animator animator) =>
            {
                movementComponent.Move = _movement;
                if(_inputAction.phase == InputActionPhase.Started)
                {
                    movementComponent.LookDirection = _movement;
                    jerkComponent.JerkDirection = _movement;
                    _direction = _movement;
                    animator.SetBool("run", true);
                    animator.SetBool("idle", false);
                }
                else
                {
                    animator.SetBool("idle", true);
                    animator.SetBool("run", false);
                }
            }).WithoutBurst().Run();

        if (_shootInput > 0f)
        {
            Entities.ForEach(
                (Entity entity, in Transform transform, in Rotation rotation, in WeaponComponent weaponComponent) =>
                {
                    weaponComponent.Shoot(transform, transform.rotation);
                }).WithoutBurst().Run();

            
        }

        Entities.ForEach(
            (ref BulletComponent bulletComponent) =>
            {
                if (!bulletComponent.BulletShooted)
                {
                    bulletComponent.Direction = _direction;
                    bulletComponent.BulletShooted = true;
                }
            }).WithoutBurst().Run();
    }
}
