using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePopup : BasePopup
{
    public void OnHelpBtn()
    {
        Managers.UI.CreateUI(UIType.HelpPopup);
    }

    public void OnMainMenuBtn()
    {
        Managers.UI.CreateUI(UIType.StartSceneUI);
    }
}
