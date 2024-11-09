using System;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class MapEditorBlock : MapBlock
{
    public Dictionary<Vector3, EventBlockDic> eventBlockDic = new Dictionary<Vector3, EventBlockDic>();     //이벤트 시 들어올 블록 Dictaionary
    public MapEditorBlock Parent;       //이벤트시 부모 블록

    /// <summary>
    ///  초기화 함수
    /// </summary>
    public void Init(int floor, Vector3 pos)
    {
        data.floor = floor;
        data.Pos = pos;
        data.upCount = 0;

        transform.position = pos;
    }

    /// <summary>
    /// 이벤트 타입 데이터 저장
    /// </summary>
    /// <param name="eventType"></param>
    public void SetData(BlockEventType eventType)
    {
        data.EventType = eventType;
        GroundRenderer.material = Managers.Material.Return(data.EventType);
    }

    /// <summary>
    /// 기본 Material을 설정하는 함수
    /// </summary>
    public void SetGround(Material material, BlockColorType colorType)
    {
        if (data.EventType != BlockEventType.None)
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

    /// <summary>
    /// 움직임 타입 Material을 설정하는 함수
    /// </summary>
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

    /// <summary>
    /// 위로 올라가는 블록 세팅
    /// </summary>
    private void OnSetFloorUp(int value)
    {
        data.upCount = value;

        SetGroundMaterial(MoveRenderer, Managers.Material.Return(BlockMoveType.Up));
        data.MoveType = BlockMoveType.Up;
    }

    /// <summary>
    /// 상호작용 타입 Material을 설정하는 함수
    /// </summary>
    public void SetInteraction(Material material, BlockInteractionType interactionType)
    {
        if (data.ColorType == BlockColorType.None)
            return;

        data.eventBlock = (int)interactionType >= 100;

        SetGroundMaterial(InteractionRenderer, material);
        data.InteractionType = interactionType;
    }

    /// <summary>
    /// 이벤트 타입 Material을 설정하는 함수
    /// </summary>
    public void SetEvent(Material material, BlockEventType eventType)
    {
        if (data.ColorType != BlockColorType.None)
            return;

        if (MapEditorManager.Instance.MapData.EventBlock == null)
            return;

        if (material == null)
        {
            if (Parent != MapEditorManager.Instance.MapData.EventBlock)
                return;

            var dic = MapEditorManager.Instance.MapData.EventBlock.eventBlockDic;
            if (dic.ContainsKey(data.Pos))
            {
                dic.Remove(data.Pos);
                Parent = null;
            }

            GroundRenderer.materials = new Material[0];
        }
        else
        {
            var dic = MapEditorManager.Instance.MapData.EventBlock.eventBlockDic;
            if (!dic.TryGetValue(data.Pos, out var evnetBlockList))
            {
                dic.Add(data.Pos, new EventBlockDic(data.Pos, eventType));
            }

            GroundRenderer.material = material;
            Parent = MapEditorManager.Instance.MapData.EventBlock;
        }

        data.EventType = eventType;
    }

    /// <summary>
    /// Material 설정해주는 함수
    /// </summary>
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

    /// <summary>
    /// 맵에 하나만 있어야하는 블록들 설정 함수
    /// </summary>
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

    /// <summary>
    /// 블록정보 리셋해주는 함수
    /// </summary>
    public void ResetBlock()
    {
        GroundRenderer.materials = new Material[0];
        MoveRenderer.materials = new Material[0];
        InteractionRenderer.materials = new Material[0];

        data.upCount = 0;

        data.ColorType = BlockColorType.None;
        data.MoveType = BlockMoveType.None;
        data.InteractionType = BlockInteractionType.None;
        data.EventType = BlockEventType.None;

        data.eventBlock = false;
        data.eventBlockList.Clear();
    }
}

