using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPopup : BasePopup
{
    public override void Init()
    {
        Time.timeScale = 0f;
        SaveData.SaveCurrentStage();
    }
    public void OnClickRetryBtn()
    {
        Time.timeScale = 1.0f;
        Close(SceneType.InGame);
    }

    public void OnClickMainMenuBtn()
    {
        Time.timeScale = 1.0f;
        Close(SceneType.Title);
    }
}
