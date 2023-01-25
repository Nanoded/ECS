using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class JerkSystem : SystemBase
{
    private InputAction _jerkAction;
    private float _jerkInput;

    protected override void OnStartRunning()
    {
        _jerkAction = new InputAction("Jerk", binding: ("<Keyboard>/Space"));
        _jerkAction.started += context => { _jerkInput = context.ReadValue<float>(); };
        _jerkAction.canceled += context => { _jerkInput = context.ReadValue<float>(); };
        _jerkAction.performed += context => { _jerkInput = context.ReadValue<float>(); };

        _jerkAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _jerkAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.ForEach(
            ( ref JerkComponent jerkComponent) =>
            {
                if(_jerkInput == 1)
                {
                    jerkComponent.Jerk = true;
                }
                else
                {
                    jerkComponent.Jerk = false;
                }
            }).WithoutBurst().Run();
    }
}
