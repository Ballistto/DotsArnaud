using Unity.Entities;
using UnityEngine;

class InputsManagerAuthoring : MonoBehaviour
{

    class InputsManagerAuthoringBaker : Baker<InputsManagerAuthoring>
    {
        public override void Bake(InputsManagerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new InputsManagerComponentData
            {
                mousePosition = Vector3.zero,
            });
        }
    }
}


