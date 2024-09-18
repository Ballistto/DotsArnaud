using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

partial struct ColorChangeBaseOnEnableSystem : ISystem, ISystemStartStop
{
    public void OnStartRunning(ref SystemState state)
    {
        foreach (var (selectable, entity)
            in SystemAPI.Query<RefRO<SelectableComponentData>>().WithPresent<SelectableComponentData>().WithEntityAccess())
        {
            state.EntityManager.SetComponentEnabled<SelectableComponentData>(entity, false);
        }
    }

    public void OnStopRunning(ref SystemState state)
    {
        throw new System.NotImplementedException();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach(var (URPColor, selectable, entity) 
            in SystemAPI.Query<RefRW<URPMaterialPropertyBaseColor>, RefRO<SelectableComponentData>>().WithPresent<SelectableComponentData>().WithEntityAccess())
        {
            URPColor.ValueRW.Value = state.EntityManager.IsComponentEnabled<SelectableComponentData>(entity) ?
                new float4(5, 0, 0, 0) : new float4(0, 0, 1, 0);
        }
    }
}
