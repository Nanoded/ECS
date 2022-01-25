using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

[GenerateAuthoringComponent]
public class ThrowingWeaponComponent : IComponentData
{ 
    public int ThrowingSpeed;
    public float3 Direction;
    public float maxLifeTime;
    [HideInInspector] public float currentLifeTime;
    [HideInInspector] public bool throwed;
}
