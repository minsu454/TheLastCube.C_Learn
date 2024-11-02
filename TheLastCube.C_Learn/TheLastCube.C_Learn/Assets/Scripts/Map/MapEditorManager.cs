using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public ObjectPoolContainer pool { get; private set; }

    [SerializeField] private GameObject mapBlockPrefab;

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
}
