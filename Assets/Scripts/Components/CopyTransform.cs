using UnityEngine;
using Unity.Transforms;
using Unity.Entities;

public class CopyTransform : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new CopyTransformToGameObject());
    }
}
