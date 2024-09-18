using UnityEngine;
using Unity.Entities;

public class RotatingAuthoring : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;

    private class Baker : Baker<RotatingAuthoring> 
    { 
        public override void Bake(RotatingAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotatingSpeedComponentData
            {
                value = authoring._rotationSpeed
            });
        }
    }
}
