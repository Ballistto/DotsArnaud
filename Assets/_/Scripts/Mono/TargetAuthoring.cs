using Unity.Entities;
using UnityEngine;

class TargetAuthoring : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _offset;

    class TargetAuthoringBaker : Baker<TargetAuthoring>
    {
        public override void Bake(TargetAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new TargetComponentData
            {
                speed = authoring._speed,
                offset = authoring._offset,
            });
        }
    }
}


