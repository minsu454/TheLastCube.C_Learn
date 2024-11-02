using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TotalMapData : MonoBehaviour
{
    private readonly List<MapFloor> mapFloorList = new List<MapFloor>();

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
            
            mapFloorList.Add(floor);
        }
    }

    public void RemoveFloor()
    {

    }

    public void RemoveAllFloor()
    {

    }

    public void ChangeMapScale()
    {

    }
}
