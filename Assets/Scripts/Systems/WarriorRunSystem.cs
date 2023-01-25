using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public partial class WarriorRunSystem : SystemBase
{
    private Vector3 _target;
    private float3 _direction;

    protected override void OnUpdate()
    {

            Entities.ForEach(
                (in PlayerComponent playerComponent, in Transform transform) =>
                {
                    _target = transform.position;
                }).WithoutBurst().Run();


            Entities.ForEach(
                (ref PhysicsVelocity physicsVelocity,  ref Translation translation, in WarriorComponent warriorComponent, in Transform transform) =>
                {
                    if (!warriorComponent.Attack)
                    {
                        _direction = (_target - transform.position).normalized;
                        _direction.y = 0;
                        physicsVelocity.Linear = _direction * warriorComponent.Speed;
                        physicsVelocity.Angular = Vector3.zero;
                    }

                    else
                    {
                        physicsVelocity.Linear = float3.zero;
                    }
                }).WithoutBurst().Run();
    }
}
