using System.Linq.Expressions;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    public Vector2 Pos;
    public int Floor;
    public MeshRenderer MyRenderer;

    public BlockColorType ColorType = BlockColorType.None;
    public BlockInteractionType InteractionType = BlockInteractionType.None;

    public void Init(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetGroundMaterial(Material material, BlockColorType colorType = BlockColorType.None)
    {
        if (material == null)
            MyRenderer.materials = new Material[0];
        else
            MyRenderer.material = material;

        ColorType = colorType;
    }

    public void SetUpMaterial(Material material)
    {
        MyRenderer.material = material;
    }

    public void SetInteractionMaterial(Material material)
    {
        MyRenderer.material = material;
    }
}
