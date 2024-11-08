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
    [SerializeField] private GameObject mapBlockPrefab;     //맵블록 프리팹

    [Header("Map")]
    public GameObject MapEditorGo;                          //맵 에디터에서 쓰는 GameObject
    public GameObject MapLookGo;                            //맵을 3d로 볼때 쓰는 GameObject


    public Enum EnumType { get; private set; }              //내가 선택한 enum타입
    public Material CurMaterial { get; private set; }       //내가 선택한 material

    private void Awake()
    {
        MapData = GetComponent<TotalEditorMapData>();

        mapBlockPrefab.CreateObjectPool(9000);
    }

    private void Start()
    {
        MapData.Init();
    }

    /// <summary>
    /// Material 설정해주는 함수
    /// </summary>
    public void SetMaterial(object type)
    {
        EnumType = (Enum)type;
        CurMaterial = Managers.Material.Return(EnumType);
    }

    /// <summary>
    /// 세이브 가능한지 알려주는 함수
    /// </summary>
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
