using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

public class ThrowingWeaponSystem : SystemBase
{
    private Vector3 _targetPosition;
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;

    protected override void OnCreate()
    {
        _commandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        Entities.ForEach(
            (in PlayerComponent playerComponent, in Transform transform) =>
            {
                _targetPosition = transform.position;
            }).WithoutBurst().Run();

        Entities.ForEach(
            (Entity entity, ref PhysicsVelocity physicsVelocity, in Transform transform, in ThrowingWeaponComponent throwingWeapon) =>
            {
                if (!throwingWeapon.throwed)
                {
                    Vector3 normalizedDirection = (_targetPosition - transform.position).normalized;
                    float3 direction = new float3(normalizedDirection.x, 0, normalizedDirection.z);
                    physicsVelocity.Linear = direction * throwingWeapon.ThrowingSpeed;
                    throwingWeapon.throwed = true;
                }

                throwingWeapon.currentLifeTime += Time.DeltaTime;
                if(throwingWeapon.currentLifeTime > throwingWeapon.maxLifeTime)
                {
                    _commandBufferSystem.CreateCommandBuffer().DestroyEntity(entity);
                    MonoBehaviour.Destroy(transform.gameObject);
                }
            }).WithoutBurst().Run();
    }
}
