using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;
using RaycastHit = Unity.Physics.RaycastHit;

public partial class RaycastSystem : SystemBase
{
    private BuildPhysicsWorld _physicsWorld;
    private CollisionWorld _collisionWorld;
    private Controller _controller;
    private InputAction _mouseMovement;

    protected override void OnCreate()
    {
        _controller = new Controller();
        _mouseMovement = _controller.Player.MousePosition;
        _physicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        _controller.Enable();
    }

    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, Translation translation, JerkComponent jerk, ref Rotation rotation, in PlayerComponent player) =>
        {
            CanRotate(out var hit);
            var direction = math.normalizesafe(hit.Position);
            rotation.Value = Quaternion.LookRotation(direction, Vector3.up);
        }).WithoutBurst().Run();
    }

    private void CanRotate(out RaycastHit hit)
    {
        _collisionWorld = _physicsWorld.PhysicsWorld.CollisionWorld;
        var ray = Camera.main.ScreenPointToRay(_mouseMovement.ReadValue<Vector2>());
        var rayStart = ray.origin;
        var rayEnd = ray.GetPoint(500);

        Raycast(rayStart, rayEnd, out hit);

    }

    private void Raycast(float3 rayStart, float3 rayEnd, out RaycastHit hit)
    {
        var raycastInput = new RaycastInput
        {
            Start = rayStart,
            End = rayEnd,
            Filter = new CollisionFilter
            {
                BelongsTo = 1 << 5,
                CollidesWith = 1 << 6,
            }
        };

        _collisionWorld.CastRay(raycastInput, out hit);
    }
}
