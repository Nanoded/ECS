using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct BulletComponent : IComponentData
{
    public float Speed;
    public float2 Direction;
    public float MaxLifeTime;
    [HideInInspector] public float CurrentLifeTime;
    [HideInInspector] public bool BulletShooted;
}
