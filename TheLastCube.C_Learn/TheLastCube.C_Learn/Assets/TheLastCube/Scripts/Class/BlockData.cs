using System;
using UnityEngine;

[Serializable]
public class BlockData
{
    public int floor;
    public Vector3 Pos;

    public BlockColorType ColorType = BlockColorType.None;
    public BlockMoveType MoveType = BlockMoveType.None;
    public BlockInteractionType InteractionType = BlockInteractionType.None;
}