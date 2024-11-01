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
                    GameObject go = new GameObject("MapManager");
                    instance = go.AddComponent<MapEditorManager>();
                }
            }
            return instance;
        }
    }

    public TotalMapData MapData { get; private set; }

    private void Awake()
    {
        MapData = instance.GetComponent<TotalMapData>();
    }
}
