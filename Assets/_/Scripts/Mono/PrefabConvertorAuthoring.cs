using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


public class PrefabConvertorAuthoring : MonoBehaviour
{
    public GameObject m_prefab;
    [SerializeField]
    private int _sizeX;
    [SerializeField]
    private int _sizeY;
    [SerializeField]
    private float _spawnSpace;

    private class Baker : Baker<PrefabConvertorAuthoring>
    {
        public override void Bake(PrefabConvertorAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new PrefabCollectionComponentData
            {
                prefab = GetEntity(authoring.m_prefab, TransformUsageFlags.None),
                sizeX = authoring._sizeX,
                sizeY = authoring._sizeY,
                spawnSpace = authoring._spawnSpace,
            });
        }
    }
}

public struct PrefabCollectionComponentData : IComponentData
{
    public Entity prefab;
    public int sizeX;
    public int sizeY;
    public float spawnSpace;
}
