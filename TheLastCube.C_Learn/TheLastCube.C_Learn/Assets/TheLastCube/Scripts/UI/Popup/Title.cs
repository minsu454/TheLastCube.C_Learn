using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : BasePopup
{
    public override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// 맵고르는 화면 띄워주는 버튼 함수
    /// </summary>
    public void OnClickStartBtn()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CreateUI(UIType.MapChoicePopup, false);
    }

    /// <summary>
    /// 도움말 화면 띄워주는 버튼 함수
    /// </summary>
    public void OnClickHelpBtn()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CreateUI(UIType.HelpPopup, false);
    }

    /// <summary>
    /// 옵션 화면 띄워주는 버튼 함수
    /// </summary>
    public void OnClickOptionBtn()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CreateUI(UIType.OptionPopup, false);
    }
}
