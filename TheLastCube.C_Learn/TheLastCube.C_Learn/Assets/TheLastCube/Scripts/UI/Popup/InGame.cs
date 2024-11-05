using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : BasePopup
{
    public void PauseButton()
    {
        Managers.UI.CreateUI(UIType.PausePopup);
    }
}
