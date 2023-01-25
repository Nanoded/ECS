using Unity.Entities;
using UnityEngine;

public partial class DestroySystem : SystemBase
{
    private EntityCommandBuffer _commandBuffer;
    private EndSimulationEntityCommandBufferSystem _endSimulationBuffer;

    protected override void OnCreate()
    {
        _endSimulationBuffer = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        Entities.ForEach(
            (Entity entity, in Transform transform, in HealthKitComponent deadComponent) =>
            {
                _commandBuffer = _endSimulationBuffer.CreateCommandBuffer();
                if (deadComponent.Dead)
                {
                    _commandBuffer.DestroyEntity(entity);
                    MonoBehaviour.Destroy(transform.gameObject);
                }
            }).WithoutBurst().Run();
    }
}
