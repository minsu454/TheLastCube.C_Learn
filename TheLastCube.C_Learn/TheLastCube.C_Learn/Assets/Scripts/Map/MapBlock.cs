using System.Linq.Expressions;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    public Vector2 pos;
    public int floor;



    public void Init(Vector3 pos)
    {
        transform.position = pos;
    }
}
