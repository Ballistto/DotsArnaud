using Unity.Entities;
using UnityEngine;

class SelectableAuthoring : MonoBehaviour
{
    class SelectableAuthoringBaker : Baker<SelectableAuthoring>
    {
        public override void Bake(SelectableAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SelectableComponentData());
        }
    }
}


