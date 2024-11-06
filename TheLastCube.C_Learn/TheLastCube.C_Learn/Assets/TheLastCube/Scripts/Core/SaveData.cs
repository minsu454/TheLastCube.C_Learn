using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public static class SaveData 
{
    public static void SaveCurrentStage()
    {
        string currentStage = Managers.Data.FileName;
        PlayerPrefs.SetInt(currentStage, 1);
    }

    //public void SaveClearData(string stagename)
    //{
    //    PlayerPrefs.SetInt(stagename, 1);
    //}

    public static bool GetClearData(string stagename)
    {
        return PlayerPrefs.HasKey(stagename);
    }

    public static void GetAllClearData()
    {
        int i = 1;
        while (PlayerPrefs.HasKey($"{i}Stage")) 
        {
            Debug.Log($"{i}Stage is Clear.");
            i++;
        }
    }

    public static void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }
}
