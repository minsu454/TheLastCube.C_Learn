using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MapEditorManager : MonoBehaviour
{
    private static MapEditorManager instance;
    public static MapEditorManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<MapEditorManager>();

                if (instance == null)
                {
                    throw new UnityException("MapEditorManager");
                }
            }
            return instance;
        }
    }

    public TotalEditorMapData MapData { get; private set; }

    [Header("ObjectPool")]
    [SerializeField] private GameObject mapBlockPrefab;

    [Header("Map")]
    public GameObject MapEditorGo;
    public GameObject MapLookGo;    


    public Enum EnumType { get; private set; }
    public Material CurMaterial { get; private set; }

    private void Awake()
    {
        MapData = GetComponent<TotalEditorMapData>();

        mapBlockPrefab.CreateObjectPool(9000);
    }

    private void Start()
    {
        MapData.Init();
    }

    public void SetMaterial(object type)
    {
        EnumType = (Enum)type;
        CurMaterial = Managers.Material.Return(EnumType);
    }

    public bool CanSave()
    {
        if (MapData.StartBlock == null || MapData.EndBlock == null)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// json으로 데이터 직렬화해주는 함수
    /// </summary>
    public string DataToJson(string name)
    {
        TotalMapData blockListData = new TotalMapData();
        blockListData.list = new List<BlockData>();
        
        blockListData.name = name;
        blockListData.maxFloor = MapData.ReturnCurFloor();

        foreach (var list in MapData.SaveDic.Values)
        {
            foreach (var mapBlock in list)
            {
                mapBlock.Data.eventBlockList = mapBlock.eventBlockDic.Values.ToList();
                blockListData.list.Add(mapBlock.Data);
            }
        }

        string s = JsonUtility.ToJson(blockListData);

        return s;
    }

    /// <summary>
    /// json파일 주소 받아와서 역직렬화해주는 함수
    /// </summary>
    public void LoadData(string path)
    {
        string json = File.ReadAllText(path);
        TotalMapData blockListData = JsonUtility.FromJson<TotalMapData>(json);

        try
        {
            MapData.LoadData(blockListData);
        }
        catch
        {
            throw new ArgumentNullException();
        }
    }
}
