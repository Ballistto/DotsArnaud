using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using System;

partial struct SetPositionSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        SetPositionJob setPositionJob = new SetPositionJob();
        setPositionJob.deltaTime = SystemAPI.Time.DeltaTime;
        setPositionJob.ScheduleParallel();
    }

    partial struct SetPositionJob : IJobEntity
    {
        public float deltaTime;
        public void Execute(ref LocalTransform transform, ref SetPositionComponentData _setPositionComponentData)
        {
            _setPositionComponentData.time += deltaTime * _setPositionComponentData.speed;
            float size = _setPositionComponentData.size * math.abs(math.sin(_setPositionComponentData.time * 0.2f)) + 3;
            float positionY = size * math.sin(_setPositionComponentData.time + (_setPositionComponentData.offsetSinus * (transform.Position.x + transform.Position.z))) + _setPositionComponentData.centerY;
            transform.Position.y = positionY;
        }
    }
}
