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

    /// <summary>
    /// Retry버튼 
    /// </summary>
    public void OnClickRetryBtn()
    {
        Time.timeScale = 1.0f;
        Close(SceneType.InGame);
    }

    /// <summary>
    /// mainMenu버튼
    /// </summary>
    public void OnClickMainMenuBtn()
    {
        Time.timeScale = 1.0f;
        Close(SceneType.Title);
    }
}
