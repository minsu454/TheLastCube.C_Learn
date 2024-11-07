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
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Time.timeScale = 1f;
        Close(SceneType.Title);
    }

    public void GoResetBtn()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Time.timeScale = 1f;
        Close(SceneType.InGame);
    }

    public override void Close()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Time.timeScale = 1f;
        base.Close();
    }

    public void GoHelpPopup()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CreateUI(UIType.HelpPopup);
    }
}


