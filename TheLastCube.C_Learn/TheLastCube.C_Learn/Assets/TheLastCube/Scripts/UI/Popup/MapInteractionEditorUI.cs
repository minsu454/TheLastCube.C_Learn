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
    [SerializeField] private UIScrollView blockInteractionEventScrollView;

    public override void Init()
    {
        base.Init();

        floorInteractionUI.Init();
        blockInteractionEventPaletteUI.Init();

        blockInteractionEventScrollView.CreateItem<BlockEventType>(CustomClickEvent);

        MapEditorManager.Instance.MapData.ShowUpFloorEvent += OnSeeUpFloor;
        MapEditorManager.Instance.MapData.HideCurFloorEvent += OnHideCurFloor;

        floorText.text = MapEditorManager.Instance.MapData.ReturnCurFloor().ToString();
    }

    public void CustomClickEvent(object type)
    {
        MapEditorManager.Instance.SetMaterial(type);
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

        MapEditorManager.Instance.MapData.EventBlock = null;

        base.Close();
    }
}
