using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial class LookAtPlayer : SystemBase
{
    private Transform _playerTransform;

    protected override void OnUpdate()
    {
        Entities.ForEach(
            (in PlayerComponent player, in Transform transform) =>
            {
                _playerTransform = transform;
            }).WithoutBurst().Run();

        Entities.ForEach(
            (ref Rotation rotation, in ThrowerComponent thrower, in Transform transform) =>
            {
                var distance = _playerTransform.position - transform.position;
                var direction = distance.normalized;
                direction.y = 0;
                rotation.Value = Quaternion.LookRotation(direction, Vector3.up);
            }).WithoutBurst().Run();

        Entities.ForEach(
            (ref Rotation rotation, in WarriorComponent warrior, in Transform transform) =>
            {
                var distance = _playerTransform.position - transform.position;
                var direction = distance.normalized;
                direction.y = 0;
                rotation.Value = Quaternion.LookRotation(direction, Vector3.up);
            }).WithoutBurst().Run();
    }
}
