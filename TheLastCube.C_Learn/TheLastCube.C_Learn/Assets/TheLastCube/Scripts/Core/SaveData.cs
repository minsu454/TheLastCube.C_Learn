using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public void SaveCurrentStage()
    {
        string currentStage = Managers.Data.FileName;
        PlayerPrefs.SetInt(currentStage, 1);
    }

    //public void SaveClearData(string stagename)
    //{
    //    PlayerPrefs.SetInt(stagename, 1);
    //}

    public bool GetClearData(string stagename)
    {
        return PlayerPrefs.HasKey(stagename);
    }

    public void GetAllClearData()
    {
        int i = 1;
        while (PlayerPrefs.HasKey($"{i}Stage")) 
        {
            Debug.Log($"{i}Stage is Clear.");
        }
    }

    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }
}
