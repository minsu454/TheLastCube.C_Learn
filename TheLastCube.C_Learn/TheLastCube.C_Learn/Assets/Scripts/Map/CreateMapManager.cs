using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateMapManager : MonoBehaviour
{
    private static CreateMapManager instance;
    public static CreateMapManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("CreateMapManager");
                instance = go.AddComponent<CreateMapManager>();
            }
            return instance;
        }
    }

    public TotalMapData TotalData;
}
