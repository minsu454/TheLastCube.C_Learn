using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ResetPlayerPrefs
{
    [MenuItem("Tools/PlayerPrefs/Reset")]
    private static void Reset()
    {
        SaveData.DeleteAllData();
        Debug.Log("Reset");
    }
}
