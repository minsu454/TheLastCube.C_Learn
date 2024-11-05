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

    private void OnSeeUpFloor(int value)
    {
        floorText.text = (value + 1).ToString();
    }

    private void OnHideCurFloor(int value)
    {
        floorText.text = (value + 1).ToString();
    }

    public void SeeMapLookUI()
    {
        Managers.UI.CreateUI(UIType.MapLookUI);
    }

    public void SeeUpFloorBtn()
    {
        MapEditorManager.Instance.MapData.ShowUpFloor();
    }

    public void HideCurFloorBtn()
    {
        MapEditorManager.Instance.MapData.HideCurFloor();
    }

    public override void Close()
    {
        MapEditorManager.Instance.MapData.ShowUpFloorEvent -= OnSeeUpFloor;
        MapEditorManager.Instance.MapData.HideCurFloorEvent -= OnHideCurFloor;

        MapEditorManager.Instance.MapData.EventBlock = null;

        base.Close();
    }
}
