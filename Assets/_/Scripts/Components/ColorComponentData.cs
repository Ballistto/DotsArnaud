using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

[MaterialProperty("_Color")]
public struct ColorComponentData : IComponentData
{
    public float colorChangeTime;
    public float currentTime;

    public float4 color1;
    public float4 color2;

    public bool changeColorByPosition;

    public float maximumY;
    public float minimumY;
}
