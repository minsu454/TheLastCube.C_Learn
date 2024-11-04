using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public TotalMapData MapData { get; private set; }

    [Header("ObjectPool")]
    [SerializeField] private GameObject mapBlockPrefab;

    [Header("Map")]
    public GameObject MapEditorGo;
    public GameObject MapLookGo;    


    public Enum EnumType { get; private set; }
    public Material CurMaterial { get; private set; }

    private void Awake()
    {
        MapData = GetComponent<TotalMapData>();

        mapBlockPrefab.CreateObjectPool(9000);

        Managers.UI.CreateUI(UIType.MapEditorUI);
    }

    private void Start()
    {
        MapData.Init();
    }

    public void SetMaterial(Enum type)
    {
        EnumType = type;
        CurMaterial = Managers.Material.Return(type);
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
        BlockListData blockListData = new BlockListData();
        blockListData.list = new List<BlockData>();
        
        blockListData.name = name;
        blockListData.maxFloor = MapData.ReturnCurFloor();

        foreach (var list in MapData.SaveDic.Values)
        {
            foreach (var mapBlock in list)
            {
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
        BlockListData blockListData = JsonUtility.FromJson<BlockListData>(json);

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
