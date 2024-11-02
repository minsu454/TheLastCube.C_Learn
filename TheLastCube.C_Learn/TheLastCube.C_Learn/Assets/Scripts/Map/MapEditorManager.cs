using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

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
    public MapCameraManager CameraManager { get; private set; }

    [SerializeField] private GameObject mapBlockPrefab;

    private void Awake()
    {
        MapData = GetComponent<TotalMapData>();
        CameraManager = GetComponent<MapCameraManager>();

        mapBlockPrefab.CreateObjectPool(9000);

        CreateGameObject<MapController>();

        Managers.UI.CreateUI(UIType.MapEditorUI);
    }

    private void CreateGameObject<T>() where T : MonoBehaviour
    {
        GameObject go = new GameObject(typeof(T).Name);
        go.AddComponent<T>();
    }

    private void Start()
    {
        MapData.Init();
    }
}
