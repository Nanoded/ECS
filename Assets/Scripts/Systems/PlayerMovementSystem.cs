using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial class PlayerMovementSystem : SystemBase
{
    private float _timeAfterStartJerk;

    protected override void OnStartRunning()
    {
        _timeAfterStartJerk = 0;
    }

    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        Entities.ForEach(
            (Entity entity, ref Translation translation, ref MovementComponent movementComponent, ref JerkComponent jerkComponent) =>
            {
                if (jerkComponent.Jerk == true && UnityEngine.Time.time > jerkComponent.Timer + jerkComponent.TimeReload)
                {
                    if(jerkComponent.JerkDirection.Equals(float2.zero))
                    {
                        jerkComponent.JerkDirection = new float2(0, 1);
                    }

                    translation.Value += new float3(jerkComponent.JerkDirection.x, 0, jerkComponent.JerkDirection.y) * deltaTime * jerkComponent.JerkPower;
                    _timeAfterStartJerk += Time.DeltaTime;

                    if (_timeAfterStartJerk >= jerkComponent.TimeJerk)
                    {
                        jerkComponent.Timer = UnityEngine.Time.time;
                        _timeAfterStartJerk = 0;
                    }
                }
                else
                {
                    translation.Value += new float3(movementComponent.Move.x, 0, movementComponent.Move.y) * movementComponent.Speed * deltaTime;
                }

            }).WithoutBurst().Run();
            ;
    }
}
