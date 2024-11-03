using System.Linq.Expressions;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    private BlockData data = new BlockData();

    [SerializeField] private MeshRenderer GroundRenderer;
    [SerializeField] private MeshRenderer MoveRenderer;
    [SerializeField] private MeshRenderer InteractionRenderer;

    public void Init(Vector3 pos)
    {
        data.Pos = pos;
        transform.position = pos;

        ResetBlock();
    }

    public void SetGroundMaterial(Material material, BlockColorType colorType)
    {
        if (material == null)
        {
            MapEditorManager.Instance.MapData.RemoveSave(this);
            ResetBlock();
        }
        else
        {
            MapEditorManager.Instance.MapData.AddSave(this);
            GroundRenderer.material = material;
        }

        data.ColorType = colorType;
    }

    public void SetGroundMaterial(Material material, BlockMoveType moveType)
    {
        if (data.ColorType == BlockColorType.None)
            return;

        if (material == null)
        {
            MoveRenderer.materials = new Material[0];
        }
        else
        {
            MoveRenderer.material = material;
        }


        data.MoveType = moveType;
    }

    public void SetGroundMaterial(Material material, BlockInteractionType interactionType)
    {
        if (data.ColorType == BlockColorType.None)
            return;

        if (material == null)
        {
            InteractionRenderer.materials = new Material[0];
        }
        else
        {
            InteractionRenderer.material = material;
        }

        data.InteractionType = interactionType;
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
