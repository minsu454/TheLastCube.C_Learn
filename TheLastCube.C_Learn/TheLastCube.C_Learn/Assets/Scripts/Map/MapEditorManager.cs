using ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
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
}
