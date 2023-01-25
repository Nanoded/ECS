using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class LookForwardSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach(
            (ref Rotation rotation, in MovementComponent movementComponent) =>
            {
                float3 inputDirection = new float3(movementComponent.LookDirection.x, 0, movementComponent.LookDirection.y);
                float3 direction = math.normalizesafe(inputDirection);
                rotation.Value = Quaternion.LookRotation(direction, Vector3.up);
            }).Run() ;
    }
}
