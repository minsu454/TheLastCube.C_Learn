using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 블록의 데이터 class
/// </summary>
[Serializable]
public class BlockData
{
    public int floor;           //층
    public Vector3 Pos;         //위치

    public int upCount;         //블록이 위로 올라가는 카운트
    public bool eventBlock;     //이벤트가 있는 블록인지 체크하는 변수

    public List<EventBlockDic> eventBlockList = new List<EventBlockDic>();      //이벤트에 관련된 블록들 저장하는 list

    public BlockColorType ColorType = BlockColorType.None;                      //블록 색깔 타입
    public BlockMoveType MoveType = BlockMoveType.None;                         //움직이는 기믹타입
    public BlockInteractionType InteractionType = BlockInteractionType.None;    //상호작용타입
    public BlockEventType EventType = BlockEventType.None;                      //이벤트 타입
}

/// <summary>
/// json 직렬화에 필요한 이벤트 블록 정보 class
/// </summary>
[Serializable]
public class EventBlockDic
{
    public Vector3 Key;             //블록위치
    public BlockEventType Value;    //블록 이벤트 타입

    public EventBlockDic(Vector3 Key, BlockEventType Value)
    {
        this.Key = Key;
        this.Value = Value;
    }
}