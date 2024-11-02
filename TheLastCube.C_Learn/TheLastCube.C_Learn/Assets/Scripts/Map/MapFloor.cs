using UnityEngine;
using System.Collections.Generic;
using ObjectPool;

public class MapFloor : MonoBehaviour
{
    private readonly List<List<MapBlock>> mapfloorLists = new List<List<MapBlock>>();

    private const string poolName = "MapBlock";

    public void Create(int mapScaleX, int mapScaleZ, int curfloor, Transform parent)
    {
        List<MapBlock> list = new List<MapBlock>();
        for (int x = 0; x < mapScaleX; x++)
        {
            for (int z = 0; z < mapScaleZ; z++)
            {
                GameObject go = ObjectPoolContainer.Instance.Pop(poolName);
                MapBlock mapBlock = go.GetComponent<MapBlock>();
                mapBlock.transform.parent = parent;

                mapBlock.Init(new Vector3(x, curfloor, z));
                go.SetActive(true);
            }
        }

        mapfloorLists.Add(list);
    }
}