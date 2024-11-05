using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopup : BasePopup
{
    public override void Init()
    {
        base.Init();
        Time.timeScale = 0f;
    }

    public void GoTitleBtn()
    {
        Time.timeScale = 1f;
        Close(SceneType.Title);
    }

    public void GoResetBtn()
    {
        Time.timeScale = 1f;
        Close(SceneType.InGame);
    }

    public override void Close()
    {
        Time.timeScale = 1f;
        base.Close();
    }
}


