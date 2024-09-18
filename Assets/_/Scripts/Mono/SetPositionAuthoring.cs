using Unity.Entities;
using UnityEngine;

class SetPositionAuthoring : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _centerY;
    [SerializeField]
    private float _size;
    [SerializeField]
    private float _offsetSinus;

    class SetPositionAuthoringBaker : Baker<SetPositionAuthoring>
    {
        public override void Bake(SetPositionAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SetPositionComponentData
            {
                speed = authoring._speed,
                centerY = authoring._centerY,
                size = authoring._size,
                offsetSinus = authoring._offsetSinus,
            });
        }
    }
}


