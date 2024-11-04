using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlockData
{
    public int floor;
    public Vector3 Pos;

    public int upCount;
    public bool eventBlock;
    public readonly List<MapEditorBlock> eventBlockList = new List<MapEditorBlock>();

    public BlockColorType ColorType = BlockColorType.None;
    public BlockMoveType MoveType = BlockMoveType.None;
    public BlockInteractionType InteractionType = BlockInteractionType.None;
}