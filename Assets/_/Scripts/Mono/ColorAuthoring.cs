using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Rendering;

class ColorAuthoring : MonoBehaviour
{
    [SerializeField]
    private float _colorChangeTime;
    [SerializeField]
    private Color _color1;
    [SerializeField]
    private Color _color2;
    [SerializeField]
    private bool _changeColorByPosition;
    [SerializeField]
    private float _maximumY;
    [SerializeField]
    private float _minimumY;

    private class ColorAuthoringBaker : Baker<ColorAuthoring>
    {
        public override void Bake(ColorAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ColorComponentData
            {
                colorChangeTime = authoring._colorChangeTime,
                currentTime = 0,

                color1 = new float4(authoring._color1.r, authoring._color1.g, authoring._color1.b, authoring._color1.a),
                color2 = new float4(authoring._color2.r, authoring._color2.g, authoring._color2.b, authoring._color2.a),
                changeColorByPosition = authoring._changeColorByPosition,
                maximumY = authoring._maximumY,
                minimumY = authoring._minimumY,
            });

            AddComponent(entity, new URPMaterialPropertyBaseColor
            {
                Value = new float4(authoring._color1.r, authoring._color1.g, authoring._color1.b, authoring._color1.a),
            });
        }
    }

}
