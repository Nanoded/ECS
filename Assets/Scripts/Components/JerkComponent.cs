using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct JerkComponent : IComponentData
{
    public bool Jerk;
    public float JerkPower;
    public float TimeReload;
    public float TimeJerk;
    public float Timer;
    public float2 JerkDirection;
} 
