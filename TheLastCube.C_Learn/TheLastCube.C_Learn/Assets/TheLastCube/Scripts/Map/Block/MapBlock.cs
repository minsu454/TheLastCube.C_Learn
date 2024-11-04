using System.Linq.Expressions;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    public BlockData data;
    public BlockData Data { get { return data; } }

    [SerializeField] protected MeshRenderer GroundRenderer;
    [SerializeField] protected MeshRenderer MoveRenderer;
    [SerializeField] protected MeshRenderer InteractionRenderer;

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

    public void SetData(BlockEventType eventType)
    {
        data.EventType = eventType;
        GroundRenderer.material = Managers.Material.Return(data.EventType);
    }
}

