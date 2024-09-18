using System.Diagnostics;
using System.Numerics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial struct TargetSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        TargetJob job = new TargetJob();
        job.deltaTime = SystemAPI.Time.DeltaTime;
        job.inputsData = SystemAPI.GetSingleton<InputsManagerComponentData>();
        job.ScheduleParallel();
    }

    partial struct TargetJob : IJobEntity
    {
        public float deltaTime;
        public InputsManagerComponentData inputsData;
        public void Execute(ref LocalTransform transform, in TargetComponentData _targetData)
        {
            float3 mousePosition = new float3(inputsData.mousePosition.x, 1, inputsData.mousePosition.z);
            float distance = math.distance(transform.Position, mousePosition);
            if (distance < _targetData.offset) return;

            float3 direction = (mousePosition - transform.Position) / distance;
            transform.Position += direction * _targetData.speed * deltaTime;

            quaternion rotation = quaternion.LookRotation(direction, new float3(0, 1, 0));
            transform.Rotation = rotation;
        }
    }
}
