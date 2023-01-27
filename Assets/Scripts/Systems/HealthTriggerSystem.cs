using Unity.Entities;
using Unity.Physics;
using Unity.Jobs;
using Unity.Physics.Systems;
using Unity.Burst;
using UnityEngine.Jobs;

public class HealthTriggerSystem : ComponentSystemBase
{
    private BuildPhysicsWorld _buildPhysicsSystem;
    private StepPhysicsWorld _stepPhysicsSystem;
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;


    protected override void OnCreate()
    {
        _buildPhysicsSystem = World.GetExistingSystem<BuildPhysicsWorld>();
        _stepPhysicsSystem = World.GetExistingSystem<StepPhysicsWorld>();
        _commandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    public override void Update()
    {
        HealthTriggerJob triggerJob = new HealthTriggerJob();
    
        triggerJob.HealthComponentGroup = GetComponentDataFromEntity<HealthComponent>();
        triggerJob.HealthKitGroup = GetComponentDataFromEntity<HealthKitComponent>();
        JobHandle jobHandle = triggerJob.Schedule(_stepPhysicsSystem.Simulation, new JobHandle());
        _commandBufferSystem.AddJobHandleForProducer(jobHandle);
        jobHandle.Complete();
    }


    [BurstCompile]
    public struct HealthTriggerJob : ITriggerEventsJob
    {
        public ComponentDataFromEntity<HealthComponent> HealthComponentGroup;
        public ComponentDataFromEntity<HealthKitComponent> HealthKitGroup;

        public void Execute(TriggerEvent triggerEvent)
        {
            if (HealthComponentGroup.HasComponent(triggerEvent.EntityA) && HealthKitGroup.HasComponent(triggerEvent.EntityB))
            {
                var healthComponent = HealthComponentGroup[triggerEvent.EntityA];
                healthComponent.Health += 10;
                HealthComponentGroup[triggerEvent.EntityA] = healthComponent;

                var destroy = HealthKitGroup[triggerEvent.EntityB];
                destroy.Dead = true;
                HealthKitGroup[triggerEvent.EntityB] = destroy;
            }

            if (HealthComponentGroup.HasComponent(triggerEvent.EntityB) && HealthKitGroup.HasComponent(triggerEvent.EntityA))
            {
                var healthComponent = HealthComponentGroup[triggerEvent.EntityB];
                healthComponent.Health += 10;
                HealthComponentGroup[triggerEvent.EntityB] = healthComponent;

                var destroy = HealthKitGroup[triggerEvent.EntityA];
                destroy.Dead = true;
                HealthKitGroup[triggerEvent.EntityA] = destroy;
            }
        }
    }
}
