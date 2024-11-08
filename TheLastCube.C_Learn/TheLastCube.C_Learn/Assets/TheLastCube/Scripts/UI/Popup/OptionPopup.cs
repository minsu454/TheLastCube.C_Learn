using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : BasePopup
{
    [SerializeField] private UISoundBar uiSoundBar;     //사운드 바

    public override void Init()
    {
        base.Init();

        uiSoundBar.Init();
    }
}
