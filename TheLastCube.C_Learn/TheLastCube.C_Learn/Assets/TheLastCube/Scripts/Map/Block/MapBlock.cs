using System.Linq.Expressions;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    public BlockData data = new BlockData();
    public BlockData Data { get { return data; } }

    [SerializeField] protected MeshRenderer GroundRenderer;
    [SerializeField] protected MeshRenderer MoveRenderer;
    [SerializeField] protected MeshRenderer InteractionRenderer;

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

    public void BlockInteraction()
    {
        if (!data.eventBlock)
        {
            return;
        }

        foreach(var d in data.eventBlockList)
        {
            
        }

        //foreach (var eventBlockData in blockData.eventBlockList)
        //{
        //    GameObject eventClone = Instantiate(mapBlockPrefab[(int)BlockPrefabNameType.MapBlock]);
        //    MapBlock eventBlock = eventClone.GetComponent<MapBlock>();

        //    eventClone.transform.position = eventBlockData.Key;
        //    eventBlock.SetData(eventBlockData.Value);
        //}
    }
}

