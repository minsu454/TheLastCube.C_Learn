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

    public List<EventBlockDic> eventBlockList = new List<EventBlockDic>();

    public BlockColorType ColorType = BlockColorType.None;
    public BlockMoveType MoveType = BlockMoveType.None;
    public BlockInteractionType InteractionType = BlockInteractionType.None;
    public BlockEventType EventType = BlockEventType.None;
}

[Serializable]
public class EventBlockDic
{
    public Vector3 Key;
    public BlockEventType Value;

    public EventBlockDic(Vector3 Key, BlockEventType Value)
    {
        this.Key = Key;
        this.Value = Value;
    }
}