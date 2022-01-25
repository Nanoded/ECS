using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct MovementComponent: IComponentData
{
    public float Speed;
    [HideInInspector] public float2 Move;
    [HideInInspector] public float2 LookDirection;
}
