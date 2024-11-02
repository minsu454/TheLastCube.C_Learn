using UnityEngine;
using System.Collections.Generic;
using ObjectPool;

public class MapFloor : MonoBehaviour
{
    public readonly List<List<MapBlock>> mapfloorLists = new List<List<MapBlock>>();

    private const string poolName = "MapBlock";

    public void Create(int mapScaleX, int mapScaleZ, int curfloor, Transform parent)
    {
        List<MapBlock> list = new List<MapBlock>();
        for (int x = 0; x < mapScaleX; x++)
        {
            for (int y = 0; y < mapScaleZ; y++)
            {
                GameObject go = ObjectPoolContainer.Instance.Pop(poolName);
                MapBlock mapBlock = go.GetComponent<MapBlock>();

                mapBlock.Init(new Vector3(mapScaleX, curfloor, mapScaleZ));
                go.SetActive(true);
            }
        }

        mapfloorLists.Add(list);
    }
}