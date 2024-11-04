using UnityEngine;
using System.Collections.Generic;
using ObjectPool;

public class MapFloor : MonoBehaviour
{
    private readonly Dictionary<Vector3, MapEditorBlock> mapfloorDic = new Dictionary<Vector3, MapEditorBlock>();

    private const string poolName = "NoneMapBlock";

    public void Create(int mapScaleX, int mapScaleZ, int curfloor, Transform parent)
    {
        for (int x = 0; x < mapScaleX; x++)
        {
            for (int z = 0; z < mapScaleZ; z++)
            {
                GameObject go = ObjectPoolContainer.Instance.Pop(poolName);
                MapEditorBlock mapBlock = go.GetComponent<MapEditorBlock>();
                mapBlock.transform.parent = parent;
                Vector3 pos = new Vector3(x, curfloor, z);
                mapBlock.Init(curfloor, pos);

                go.SetActive(true);
                mapfloorDic.Add(pos, mapBlock);
            }
        }
    }

    public MapEditorBlock Return(Vector3 keyVec)
    {
        if (!mapfloorDic.TryGetValue(keyVec, out MapEditorBlock mapBlock))
        {
            Debug.LogError($"Is Not Found MapBlock : {keyVec}");
            return null;
        }

        return mapBlock;
    }
}