using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public partial class InputsManagerSystem : SystemBase
{
    private Camera _camera;
    private Vector3 _mousePosition;
    private Entity _entity;
    private EntityManager _entityManager;
    private InputsManagerComponentData _componentData;

    protected override void OnUpdate()
    {
        var inputsManagerData = SystemAPI.GetSingleton<InputsManagerComponentData>();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                inputsManagerData.mousePosition = hit.point;
                SystemAPI.SetSingleton(inputsManagerData);
            }
        }
    }
}
