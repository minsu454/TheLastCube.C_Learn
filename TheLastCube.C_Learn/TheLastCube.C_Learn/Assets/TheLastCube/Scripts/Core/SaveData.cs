using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public static class SaveData 
{
    /// <summary>
    /// 현재 스테이지 저장해주는 함수
    /// </summary>
    public static void SaveCurrentStage()
    {
        string currentStage = Managers.Data.FileName;
        PlayerPrefs.SetInt(currentStage, 1);
    }

    /// <summary>
    /// 클리어데이터 반환해주는 함수
    /// </summary>
    public static bool GetClearData(string stagename)
    {
        return PlayerPrefs.HasKey(stagename);
    }

    /// <summary>
    /// 모든 저장 데이터 지워주는 함수
    /// </summary>
    public static void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }
}
