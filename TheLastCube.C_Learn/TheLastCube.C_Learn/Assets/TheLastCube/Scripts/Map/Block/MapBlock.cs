using System.Linq.Expressions;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    public BlockData data = new BlockData();
    public BlockData Data { get { return data; } }

    [SerializeField] protected MeshRenderer GroundRenderer;         //블록 renderer
    [SerializeField] protected MeshRenderer MoveRenderer;           //블록 움직이는 renderer
    [SerializeField] protected MeshRenderer InteractionRenderer;    //블록 상호작용 renderer

    /// <summary>
    /// 데이터 설정하는 함수
    /// </summary>
    public virtual void SetData(BlockData data)
    {
        this.data = data;
        transform.position = data.Pos;

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

