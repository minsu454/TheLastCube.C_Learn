using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorUI : BasePopup
{
    [SerializeField] public UIArrowButton inspectorUI;
    [SerializeField] public UIArrowButton floorInteractionUI;

    public override void Init()
    {
        base.Init();

        inspectorUI.Init();
        floorInteractionUI.Init();
    }
}
