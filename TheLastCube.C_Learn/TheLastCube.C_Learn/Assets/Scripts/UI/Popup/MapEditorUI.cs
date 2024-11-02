using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorUI : BasePopup
{
    [SerializeField] private GameObject NotSeeMapUI;

    [SerializeField] private TMP_Text floorText;
    [SerializeField] private UIArrowButton inspectorUI;
    [SerializeField] private UIArrowButton floorInteractionUI;

    public override void Init()
    {
        base.Init();

        inspectorUI.Init();
        floorInteractionUI.Init();
    }

    public void OnSeeUpFloor()
    {
        floorText.text = (MapEditorManager.Instance.MapData.ShowUpFloor() + 1).ToString();
    }

    public void OnHideCurFloor()
    {
        floorText.text = (MapEditorManager.Instance.MapData.HideCurFloor() + 1).ToString();
    }
}
