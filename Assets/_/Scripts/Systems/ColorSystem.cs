using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;
using Unity.Transforms;
using UnityEditor.ShaderGraph;

partial struct ColorSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        ColorJob colorJob = new ColorJob();
        colorJob.deltaTime = SystemAPI.Time.DeltaTime;
        colorJob.ScheduleParallel();

        ColorPositionJob colorPositionJob = new ColorPositionJob();
        colorPositionJob.ScheduleParallel();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    partial struct ColorJob : IJobEntity
    {
        public float deltaTime;
        public void Execute(ref ColorComponentData _colorComponentData, ref URPMaterialPropertyBaseColor _material)
        {
            if (_colorComponentData.changeColorByPosition) return;

            _colorComponentData.currentTime += deltaTime;
            float modulo = _colorComponentData.currentTime % _colorComponentData.colorChangeTime;
            float4 color = new float4(
                math.lerp(_colorComponentData.color1.x, _colorComponentData.color2.x, modulo / _colorComponentData.colorChangeTime),
                math.lerp(_colorComponentData.color1.y, _colorComponentData.color2.y, modulo / _colorComponentData.colorChangeTime),
                math.lerp(_colorComponentData.color1.z, _colorComponentData.color2.z, modulo / _colorComponentData.colorChangeTime),
                math.lerp(_colorComponentData.color1.w, _colorComponentData.color2.w, modulo / _colorComponentData.colorChangeTime));

            _material.Value = color;
        }
    }
    partial struct ColorPositionJob : IJobEntity
    {
        public void Execute(ref LocalTransform transform, ref ColorComponentData _colorComponentData, ref URPMaterialPropertyBaseColor _material)
        {
            if (!_colorComponentData.changeColorByPosition) return;

            float alpha = (transform.Position.y - _colorComponentData.minimumY) / (_colorComponentData.maximumY - _colorComponentData.minimumY);
            
            float4 color = new float4(
                math.lerp(_colorComponentData.color1.x, _colorComponentData.color2.x, alpha),
                math.lerp(_colorComponentData.color1.y, _colorComponentData.color2.y, alpha),
                math.lerp(_colorComponentData.color1.z, _colorComponentData.color2.z, alpha),
                math.lerp(_colorComponentData.color1.w, _colorComponentData.color2.w, alpha));
            _material.Value = color * 5;
        }
    }
}
