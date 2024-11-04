using System.Linq.Expressions;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    private BlockData data = new BlockData();
    public BlockData Data { get {return data;} }

    [SerializeField] private MeshRenderer GroundRenderer;
    [SerializeField] private MeshRenderer MoveRenderer;
    [SerializeField] private MeshRenderer InteractionRenderer;

    public void Init(int floor, Vector3 pos)
    {
        data.floor = floor;
        data.Pos = pos;
        transform.position = pos;

        ResetBlock();
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

    public void SetGround(Material material, BlockColorType colorType)
    {
        if (material == null)
        {
            ResetBlock();
        }
        else
        {
            GroundRenderer.material = material;
        }

        data.ColorType = colorType;
    }

    public void SetMove(Material material, BlockMoveType moveType)
    {
        if (data.ColorType == BlockColorType.None)
            return;

        OnlyOneBlock(moveType, BlockMoveType.Start, ref MapEditorManager.Instance.MapData.StartBlock);
        OnlyOneBlock(moveType, BlockMoveType.End, ref MapEditorManager.Instance.MapData.EndBlock);

        SetGroundMaterial(MoveRenderer, material);
        data.MoveType = moveType;
    }

    public void SetInteraction(Material material, BlockInteractionType interactionType)
    {
        if (data.ColorType == BlockColorType.None)
            return;

        SetGroundMaterial(InteractionRenderer, material);
        data.InteractionType = interactionType;
    }

    private void SetGroundMaterial(MeshRenderer renderer, Material material)
    {
        if (material == null)
        {
            renderer.materials = new Material[0];
        }
        else
        {
            renderer.material = material;
        }
    }

    private void OnlyOneBlock(BlockMoveType curType, BlockMoveType compareType, ref MapBlock compare)
    {
        if (curType == compareType)
        {
            if (compare != null)
                compare.SetMove(null, BlockMoveType.None);

            compare = this;
        }
        else
        {
            if (compare == this)
            {
                compare = null;
            }
        }
    }

    public void ResetBlock()
    {
        GroundRenderer.materials = new Material[0];
        MoveRenderer.materials = new Material[0];
        InteractionRenderer.materials = new Material[0];

        data.ColorType = BlockColorType.None;
        data.MoveType = BlockMoveType.None;
        data.InteractionType = BlockInteractionType.None;
    }
}
