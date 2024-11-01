using UnityEngine;

public class MapGroundPoint
{
    public Vector2 pos;
    public int floor;

    public MapGroundPoint(Vector2 pos, int floor)
    {
        this.pos = pos;
        this.floor = floor;
    }
}
