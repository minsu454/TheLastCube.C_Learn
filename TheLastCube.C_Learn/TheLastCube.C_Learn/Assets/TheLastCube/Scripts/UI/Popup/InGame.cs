using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : BasePopup
{
    /// <summary>
    /// pause버튼
    /// </summary>
    public void PauseButton()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CreateUI(UIType.PausePopup,false);
    }
}
