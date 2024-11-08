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

    /// <summary>
    /// 타이틀로 이동 버튼 함수
    /// </summary>
    public void GoTitleBtn()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Time.timeScale = 1f;
        Close(SceneType.Title);
    }

    /// <summary>
    /// 맵 리셋 버튼 함수
    /// </summary>
    public void GoResetBtn()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Time.timeScale = 1f;
        Close(SceneType.InGame);
    }

    /// <summary>
    /// 옵션 화면 띄워주는 버튼 함수
    /// </summary>
    public void GoHelpPopup()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CreateUI(UIType.HelpPopup);
    }

    public override void Close()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Time.timeScale = 1f;
        base.Close();
    }
}


