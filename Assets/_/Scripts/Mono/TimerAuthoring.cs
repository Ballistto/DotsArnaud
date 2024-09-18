using Unity.Entities;
using UnityEngine;

public class TimerAuthoring : MonoBehaviour
{
    [SerializeField]
    private float _maximumTime;

    public class TimerAuthoringBaker : Baker<TimerAuthoring>
    {
        public override void Bake(TimerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new TimerComponentData
            {
                time = authoring._maximumTime
            });
        }
    }
}
