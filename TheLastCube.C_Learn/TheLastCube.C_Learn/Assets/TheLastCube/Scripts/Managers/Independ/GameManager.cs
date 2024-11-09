using ObjectPool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();

                if (instance == null)
                {
                    throw new System.Exception("Not GameObject GameManager");
                }
            }
            return instance;
        }
    }

    [SerializeField] private GameObject[] mapBlockPrefab;       //블록프리팹 Array
    [SerializeField] private GameObject playerPrefab;           //플레이어 프리팹

    private Vector3 playerSpawnPos;                             //플레이어 스폰 위치

    private readonly Dictionary<MapBlock, List<IMapEventBlock>> eventBlockDict = new Dictionary<MapBlock, List<IMapEventBlock>>();  //블록 이벤트 Dictionary

    private void Awake()
    {
        CreateMap();
        GameObject playerObj = CreatePlayer();
        CreateCameraManager(playerObj);
    }

    /// <summary>
    /// 맵 생성해주는 함수
    /// </summary>
    private void CreateMap()
    {
        string path = $"{Application.streamingAssetsPath}/MapData/{Managers.Data.FileName}.json";

        string json = File.ReadAllText(path);
        TotalMapData totalMapData = JsonUtility.FromJson<TotalMapData>(json);

        foreach (BlockData blockData in totalMapData.list)
        {
            int prefabIdx = BlockFactory.MapBlockPrefabIndex(blockData.MoveType);

            GameObject clone = Instantiate(mapBlockPrefab[prefabIdx]);
            MapBlock block = clone.GetComponent<MapBlock>();
            block.SetData(blockData);

            if (blockData.MoveType == BlockMoveType.Start)
                playerSpawnPos = blockData.Pos + Vector3.up;
            else if (blockData.eventBlock)
            {
                List<IMapEventBlock> list = new List<IMapEventBlock>();

                foreach (var eventBlockData in blockData.eventBlockList)
                {
                    int eventPrefabIdx = BlockFactory.MapBlockEventPrefabIndex(eventBlockData.Value);
                    GameObject eventClone = Instantiate(mapBlockPrefab[eventPrefabIdx]);
                    IMapEventBlock eventBlock = eventClone.GetComponent<IMapEventBlock>();

                    eventClone.transform.position = eventBlockData.Key;
                    eventBlock.SetData(eventBlockData.Value);
                    list.Add(eventBlock);
                }
                eventBlockDict.Add(block, list);
            }
        }
    }

    /// <summary>
    /// 플레이어 생성해주는 함수
    /// </summary>
    private GameObject CreatePlayer()
    {
        GameObject go = Instantiate(playerPrefab);
        go.transform.position = playerSpawnPos;

        return go;
    }

    /// <summary>
    /// 카메라 매니저 설정해주는 함수
    /// </summary>
    private void CreateCameraManager(GameObject playerObj)
    {
        CameraManager cameraManager = Camera.main.AddComponent<CameraManager>();
        cameraManager.Init(playerObj);
    }

    /// <summary>
    /// 맵 블록 이벤트 실행 함수
    /// </summary>
    public void MapBlockEventAction(MapBlock mapBlock)
    {
        if (!eventBlockDict.TryGetValue(mapBlock, out var list)) 
        {
            Debug.LogError("none dictionary key");
            return;
        }
        
        //Debug.Log(list.Count);
        for(int i =0;i<list.Count; i++)
        {
            list[i].OnEvent();
        }
    }
}
