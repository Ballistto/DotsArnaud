using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    private EntityManager _entityManager;
    private List<Entity> _entitiesSelected;

    // Start is called before the first frame update
    void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _entitiesSelected = new List<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var query = _entityManager.CreateEntityQuery(typeof(PhysicsWorldSingleton));
            var physicsSingleton = query.GetSingleton<PhysicsWorldSingleton>();

            var collisionWorld = physicsSingleton.CollisionWorld;

            RaycastInput raycastInput = new RaycastInput
            {
                Start = ray.origin,
                End = ray.GetPoint(9999),
                Filter = new CollisionFilter
                {
                    BelongsTo = ~0u,
                    CollidesWith = 1u << 6,
                    GroupIndex = 0
                }
            };

            if(collisionWorld.CastRay(raycastInput, out Unity.Physics.RaycastHit hit))
            {
                foreach (var entity in _entitiesSelected)
                {
                    _entityManager.SetComponentEnabled<SelectableComponentData>(entity, false);
                }
                _entitiesSelected.Clear();

                _entityManager.SetComponentEnabled<SelectableComponentData>(hit.Entity, true);
                _entitiesSelected.Add(hit.Entity);
            }
        }
    }
}

public struct SelectableComponentData : IComponentData , IEnableableComponent
{

}
