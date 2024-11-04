using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapInteractionEditorUI : BasePopup
{
    [SerializeField] private TMP_Text floorText;

    [Header("UIArrow")]
    [SerializeField] private UIArrowButton floorInteractionUI;
    [SerializeField] private UIArrowButton blockInteractionEventPaletteUI;

    [Header("UIScreenView")]
    [SerializeField] private UIScreenView blockInteractionEventScreenView;

    public override void Init()
    {
        base.Init();

        floorInteractionUI.Init();
        blockInteractionEventPaletteUI.Init();

        blockInteractionEventScreenView.CreateItem<BlockEventType>();

        MapEditorManager.Instance.MapData.ShowUpFloorEvent += OnSeeUpFloor;
        MapEditorManager.Instance.MapData.HideCurFloorEvent += OnHideCurFloor;

        floorText.text = MapEditorManager.Instance.MapData.ReturnCurFloor().ToString();
    }

    public void OnSeeUpFloor(int value)
    {
        floorText.text = (value + 1).ToString();
    }

    public void OnHideCurFloor(int value)
    {
        floorText.text = (value + 1).ToString();
    }

    public void SeeMapLookUI()
    {
        Managers.UI.CreateUI(UIType.MapLookUI);
    }

    public override void Close()
    {
        MapEditorManager.Instance.MapData.ShowUpFloorEvent -= OnSeeUpFloor;
        MapEditorManager.Instance.MapData.HideCurFloorEvent -= OnHideCurFloor;

        base.Close();
    }
}
