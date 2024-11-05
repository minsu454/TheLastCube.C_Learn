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

    [SerializeField] private GameObject[] mapBlockPrefab;
    [SerializeField] private GameObject playerPrefab;

    private Vector3 playerSpawnPos;

    private readonly Dictionary<MapBlock, List<IMapEventBlock>> eventBlockDict = new Dictionary<MapBlock, List<IMapEventBlock>>();

    private void Awake()
    {
        CreateMap();
        GameObject playerObj = CreatePlayer();
        CreateCameraManager(playerObj);
    }

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

    private GameObject CreatePlayer()
    {
        GameObject go = Instantiate(playerPrefab);
        go.transform.position = playerSpawnPos;

        return go;
    }

    private void CreateCameraManager(GameObject playerObj)
    {
        CameraManager cameraManager = Camera.main.AddComponent<CameraManager>();
        cameraManager.Init(playerObj);
    }

    public void MapBlockEventAction(MapBlock mapBlock)
    {
        if (!eventBlockDict.TryGetValue(mapBlock, out var list)) 
        {
            Debug.LogError("none dictionary key");
            return;
        }
        
        Debug.Log(list.Count);
        for(int i =0;i<list.Count; i++)
        {
            list[i].OnEvent();
        }
    }
}
