using UnityEngine;
using System.Collections.Generic;

public class MapFloor
{
    public readonly List<List<MapGroundPoint>> mapfloorLists = new List<List<MapGroundPoint>>();

    public void Create(int mapScaleX, int mapScaleY)
    {
        List<MapGroundPoint> list = new List<MapGroundPoint>();
        for (int x = 0; x < mapScaleX; x++)
        {
            for (int y = 0; y < mapScaleY; y++)
            {
                MapGroundPoint mapGroundPoint = new MapGroundPoint(new Vector2(x, y), mapfloorLists.Count);
            }
        }

        mapfloorLists.Add(list);
    }
}