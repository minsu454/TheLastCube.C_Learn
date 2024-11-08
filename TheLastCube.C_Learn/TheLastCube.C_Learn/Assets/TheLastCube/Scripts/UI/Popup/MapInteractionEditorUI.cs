using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapInteractionEditorUI : BasePopup
{
    [SerializeField] private TMP_Text floorText;            //층 Text

    [Header("UIArrow")]
    [SerializeField] private UIArrowButton floorInteractionUI;              //층 상호작용 설정하는 존
    [SerializeField] private UIArrowButton blockInteractionEventPaletteUI;  //이벤트 블록 만들게 설정하는 존

    [Header("UIScrollView")]
    [SerializeField] private UIScrollView blockInteractionEventScrollView;  //이벤트 블록 입히는 스크롤 뷰

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

    /// <summary>
    /// 스크롤 뷰에 버튼 입력 시 실행하는 이벤트 함수
    /// </summary>
    public void CustomClickEvent(object type)
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        MapEditorManager.Instance.SetMaterial(type);
    }

    /// <summary>
    /// 위층으로 text바꿔주는 함수
    /// </summary>
    public void OnSeeUpFloor(int value)
    {
        floorText.text = (value + 1).ToString();
    }

    /// <summary>
    /// 밑층으로 text바꿔주는 함수
    /// </summary>
    public void OnHideCurFloor(int value)
    {
        floorText.text = (value + 1).ToString();
    }

    /// <summary>
    /// 3인칭 뷰로 보여주는 함수
    /// </summary>
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
