using Unity.Entities;


[GenerateAuthoringComponent]
public struct HealthKitComponent: IComponentData
{
    public bool Dead;
}
