using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

public class BulletSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;
    private EntityCommandBuffer _commandBuffer;
    private float3 _direction;

    protected override void OnCreate()
    {
        _commandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        Entities.ForEach(
            (Entity entity, ref PhysicsVelocity velocity, ref BulletComponent bulletComponent, in Transform transform) =>
            {
                _direction = new float3(bulletComponent.Direction.x, 0, bulletComponent.Direction.y);

                velocity.Linear = _direction * bulletComponent.Speed;
                

                _commandBuffer = _commandBufferSystem.CreateCommandBuffer();
                bulletComponent.CurrentLifeTime += Time.DeltaTime;
                if(bulletComponent.CurrentLifeTime > bulletComponent.MaxLifeTime)
                {
                    _commandBuffer.DestroyEntity(entity);
                    MonoBehaviour.Destroy(transform.gameObject);
                }

            }).WithoutBurst().Run();
    }
}
