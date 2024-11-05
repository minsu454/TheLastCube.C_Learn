using ObjectPool;
using System.IO;
using System.Runtime.CompilerServices;
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

    private void Awake()
    {
        CreateMap();
        CreatePlayer();
    }

    private void CreateMap()
    {
        string path = $"{Application.streamingAssetsPath}/MapData/{Managers.Data.FileName}.json";

        string json = File.ReadAllText(path);
        TotalMapData totalMapData = JsonUtility.FromJson<TotalMapData>(json);

        Debug.Log(totalMapData.name);

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
                foreach (var eventBlockData in blockData.eventBlockList)
                {
                    GameObject eventClone = Instantiate(mapBlockPrefab[(int)BlockPrefabNameType.MapBlock]);
                    MapBlock eventBlock = eventClone.GetComponent<MapBlock>();

                    eventClone.transform.position = eventBlockData.Key;
                    eventBlock.SetData(eventBlockData.Value);
                }
            }
        }
    }

    private void CreatePlayer()
    {
        GameObject go = Instantiate(playerPrefab);
        go.transform.position = playerSpawnPos;
    }
}
