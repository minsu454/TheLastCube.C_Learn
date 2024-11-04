using System.Linq.Expressions;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    protected BlockData data = new BlockData();
    public BlockData Data { get { return data; } }

    [SerializeField] protected MeshRenderer GroundRenderer;
    [SerializeField] protected MeshRenderer MoveRenderer;
    [SerializeField] protected MeshRenderer InteractionRenderer;

    public void Init(int floor, Vector3 pos)
    {
        data.floor = floor;
        data.Pos = pos;
        data.upCount = 0;
        transform.position = pos;
    }

    public void SetData(BlockData data)
    {
        this.data = data;

        GroundRenderer.material = Managers.Material.Return(data.ColorType);

        if (data.MoveType != BlockMoveType.None)
        {
            MoveRenderer.material = Managers.Material.Return(data.MoveType);
        }

        if (data.InteractionType != BlockInteractionType.None)
        {
            InteractionRenderer.material = Managers.Material.Return(data.InteractionType);
        }
    }
}

