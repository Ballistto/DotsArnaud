using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;

public partial class SpawnSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Enabled = false;
        var prefabCollection = SystemAPI.GetSingleton<PrefabCollectionComponentData>();

        for(int i = 0; i < prefabCollection.sizeX; i++)
        {
            for (int j = 0; j < prefabCollection.sizeY; j++)
            {
                var newEntity = EntityManager.Instantiate(prefabCollection.prefab);
                EntityManager.SetComponentData(newEntity, new LocalTransform
                {
                    Position = new Vector3(i *  prefabCollection.spawnSpace, 0, j * prefabCollection.spawnSpace),
                    Rotation = Quaternion.identity,
                    Scale = 1
                });
            }
        }
    }
}
