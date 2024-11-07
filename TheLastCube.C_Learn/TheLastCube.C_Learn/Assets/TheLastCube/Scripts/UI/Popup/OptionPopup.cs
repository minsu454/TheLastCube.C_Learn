using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : BasePopup
{
    [SerializeField] private UISoundBar uiSoundBar;

    public override void Init()
    {
        base.Init();

        uiSoundBar.Init();
    }
}
