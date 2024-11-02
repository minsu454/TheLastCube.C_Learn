using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TotalMapData : MonoBehaviour
{
    private readonly List<MapFloor> mapFloorList = new List<MapFloor>();
    private readonly Stack<int> depth = new Stack<int>();

    [Range(1, 30)] public int mapScaleX = 10;      //left, right
    [Range(1, 30)] public int mapScaleZ = 10;      //forward, back

    [Range(1, 10)] public int mapScaleY = 10;      //up, down

    public void Init()
    {
        GameObject go = new GameObject("TotalMap");

        for (int i = 0; i < mapScaleY; i++)
        {
            GameObject floorGo = new GameObject($"Floor{i}");
            MapFloor floor = floorGo.AddComponent<MapFloor>();
            floor.Create(mapScaleX, mapScaleZ, mapFloorList.Count, floorGo.transform);

            floorGo.transform.parent = go.transform;
            floorGo.gameObject.SetActive(false);

            mapFloorList.Add(floor);
        }

        depth.Push(-1);
        ShowUpFloor();
        Camera.main.transform.position = new Vector3((mapScaleX / 2) - 1, 20, (mapScaleZ / 2) - 1);
    }

    public int ShowUpFloor()
    {
        int curFloorIdx = depth.Peek();

        if (curFloorIdx == mapScaleY - 1)
        {
            return curFloorIdx;
        }

        int upFloorIdx = curFloorIdx + 1;

        depth.Push(upFloorIdx);

        mapFloorList[upFloorIdx].gameObject.SetActive(true);

        return upFloorIdx;
    }

    public int HideCurFloor()
    {
        int curFloorIdx = depth.Pop();
        int downFloorIdx = depth.Peek();

        if (downFloorIdx == -1)
        {
            depth.Push(curFloorIdx);
            return curFloorIdx;
        }

        mapFloorList[curFloorIdx].gameObject.SetActive(false);
        mapFloorList[downFloorIdx].gameObject.SetActive(true);

        return downFloorIdx;
    }
}
