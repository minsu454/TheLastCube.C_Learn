using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TotalMapData : MonoBehaviour
{
    private readonly List<MapFloor> mapFloorList = new List<MapFloor>();

    public int limitXZScale { get; private set; } = 40;
    public int limitYScale { get; private set; } = 3;

    [SerializeField] private int mapScaleX;
    [SerializeField] private int mapScaleY;

    [SerializeField] private GameObject mapGroundPrefab;

    public void SetMapScale(int mapScaleX, int mapScaleY)
    {
        this.mapScaleX = mapScaleX;
        this.mapScaleY = mapScaleY;

        if (mapFloorList.Count == 0)
        {
            AddFloor();
        }
    }

    public void AddFloor()
    {
        MapFloor floor = new MapFloor();
        floor.Create(mapScaleX, mapScaleY);

        mapFloorList.Add(floor);
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
