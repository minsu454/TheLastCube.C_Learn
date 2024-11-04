using System;
using UnityEngine;

public class MapEditorBlock : MapBlock
{
    public void SetGround(Material material, BlockColorType colorType)
    {
        if (data.eventBlock)
            return;

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

        if (moveType == BlockMoveType.Up)
        {
            ChoiceFloorPopup popup = Managers.UI.CreateUI(UIType.ChoiceFloorPopup, false) as ChoiceFloorPopup;
            popup.ReturnValueEvent += OnSetFloorUp;
        }
        else
        {
            data.upCount = 0;
            SetGroundMaterial(MoveRenderer, material);
            data.MoveType = moveType;
        }
    }

    private void OnSetFloorUp(int value)
    {
        data.upCount = value;

        SetGroundMaterial(MoveRenderer, Managers.Material.Return(BlockMoveType.Up));
        data.MoveType = BlockMoveType.Up;
    }

    public void SetInteraction(Material material, BlockInteractionType interactionType)
    {
        if (data.ColorType == BlockColorType.None)
            return;

        SetGroundMaterial(InteractionRenderer, material);
        data.InteractionType = interactionType;
    }

    public void SetEvent(Material material, BlockEventType eventType)
    {
        if (data.ColorType != BlockColorType.None)
            return;

        if (material == null)
        {
            GroundRenderer.materials = new Material[0];
            data.eventBlock = false;
        }
        else
        {
            GroundRenderer.material = material;
            data.eventBlock = true;
        }
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

    private void OnlyOneBlock(BlockMoveType curType, BlockMoveType compareType, ref MapEditorBlock compare)
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

        data.upCount = 0;

        data.ColorType = BlockColorType.None;
        data.MoveType = BlockMoveType.None;
        data.InteractionType = BlockInteractionType.None;
    }
}

