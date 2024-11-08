using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TotalEditorMapData : MonoBehaviour
{
    private readonly Dictionary<int, HashSet<MapEditorBlock>> saveDic = new Dictionary<int, HashSet<MapEditorBlock>>();     //저장하려는 블록들 모아두는 Dictionary
    public Dictionary<int, HashSet<MapEditorBlock>> SaveDic { get { return saveDic; } }

    private readonly List<MapFloor> mapFloorList = new List<MapFloor>();        //맵에 있는 층들 담아주는 list
    private readonly Stack<int> depth = new Stack<int>();                       //현재 층수 저장 스텍

    public MapEditorBlock StartBlock;               //시작블록
    public MapEditorBlock EndBlock;                 //끝나는블록
    public MapEditorBlock EventBlock;               //이벤트 삽입해줄 블록

    [Range(1, 30)] public int MapScaleX = 10;       //left, right
    [Range(1, 30)] public int MapScaleZ = 10;       //forward, back
    [Range(1, 10)] public int MapScaleY = 10;       //up, down

    public event Action<int> ShowUpFloorEvent;      //지금층에 위층을 보여주는 이벤트
    public event Action<int> HideCurFloorEvent;     //지금 층을 숨겨주는 이벤트

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init()
    {
        GameObject go = new GameObject("TotalMap");

        for (int i = 0; i < MapScaleY; i++)
        {
            GameObject floorGo = new GameObject($"Floor{i}");
            MapFloor floor = floorGo.AddComponent<MapFloor>();
            floor.Create(MapScaleX, MapScaleZ, mapFloorList.Count, floorGo.transform);

            floorGo.transform.parent = go.transform;
            floorGo.gameObject.SetActive(false);

            mapFloorList.Add(floor);
        }

        depth.Push(-1);
        ShowUpFloor();
        Camera.main.transform.position = new Vector3((MapScaleX / 2) - 1, 20, (MapScaleZ / 2) - 1);
    }

    /// <summary>
    /// 지금층에 위층 보여주는 함수
    /// </summary>
    public void ShowUpFloor()
    {
        int curFloorIdx = depth.Peek();

        if (curFloorIdx == MapScaleY - 1)
        {
            return;
        }

        int upFloorIdx = curFloorIdx + 1;
        
        depth.Push(upFloorIdx);

        mapFloorList[upFloorIdx].gameObject.SetActive(true);

        ShowUpFloorEvent?.Invoke(upFloorIdx);
    }

    /// <summary>
    /// 지금 층을 숨겨주는 함수
    /// </summary>
    public void HideCurFloor()
    {
        int curFloorIdx = depth.Pop();
        int downFloorIdx = depth.Peek();

        if (downFloorIdx == -1)
        {
            depth.Push(curFloorIdx);
            return;
        }

        mapFloorList[curFloorIdx].gameObject.SetActive(false);
        mapFloorList[downFloorIdx].gameObject.SetActive(true);

        HideCurFloorEvent?.Invoke(downFloorIdx);
    }

    /// <summary>
    /// 현재층 반환 함수
    /// </summary>
    /// <returns></returns>
    public int ReturnCurFloor()
    {
        return depth.Peek();
    }

    /// <summary>
    /// 세이브 Dictionary에 저장 함수
    /// </summary>
    public void AddSave(int floor, MapEditorBlock block)
    {
        if (!saveDic.TryGetValue(floor, out var hashSet))
        {
            hashSet = new HashSet<MapEditorBlock>();
            saveDic[floor] = hashSet;
        }

        if (hashSet.Contains(block))
            return;

        saveDic[floor].Add(block);
    }

    /// <summary>
    /// 세이브 Dictionary에서 지워주는 함수
    /// </summary>
    public void RemoveSave(int floor, MapEditorBlock block)
    {
        if (!saveDic.TryGetValue(floor, out var hashSet))
            return;

        saveDic[floor].Remove(block);
    }

    public void Clear()
    {
        foreach (var list in saveDic.Values)
        {
            foreach (var mapBlock in list)
            {
                mapBlock.ResetBlock();
            }
        }

        StartBlock = null;
        EndBlock = null;

        saveDic.Clear();
    }

    /// <summary>
    /// 저장된 데이터를 로드해주는 함수
    /// </summary>
    public void LoadData(TotalMapData blockListData)
    {
        Clear();

        for (int i = 0; i < blockListData.maxFloor; i++)
        {
            ShowUpFloor();
        }

        foreach (var blockData in blockListData.list)
        {
            MapEditorBlock mapBlock = mapFloorList[blockData.floor].Return(blockData.Pos);
            
            if (blockData.MoveType == BlockMoveType.Start)
                StartBlock = mapBlock;
            else if (blockData.MoveType == BlockMoveType.End)
                EndBlock = mapBlock;
            else if (blockData.eventBlock)
            {
                foreach (var eventBlockData in blockData.eventBlockList)
                {
                    MapEditorBlock eventBlock = mapFloorList[(int)eventBlockData.Key.y].Return(eventBlockData.Key);
                    eventBlock.Parent = mapBlock;
                    mapBlock.eventBlockDic.Add(eventBlockData.Key, eventBlockData);
                    eventBlock.SetData(eventBlockData.Value);
                }
            }

            mapBlock.SetData(blockData);
            AddSave(blockData.floor, mapBlock);
        }
    }
}
