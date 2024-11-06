using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : BasePopup
{
    public override void Init()
    {
        base.Init();
    }

    public void OnClickStartBtn()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CreateUI(UIType.MapChoicePopup);
    }

    public void OnClickHelpBtn()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CreateUI(UIType.HelpPopup);
    }
}
