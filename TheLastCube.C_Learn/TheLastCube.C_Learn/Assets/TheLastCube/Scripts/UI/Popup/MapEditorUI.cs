using SimpleFileBrowser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorUI : BasePopup
{
    [SerializeField] private TMP_Text floorText;        //층 text

    [Header("UIArrow")]
    [SerializeField] private UIArrowButton saveDataUI;                  //세이브 데이터 있는 존
    [SerializeField] private UIArrowButton floorInteractionUI;          //층 상호작용 설정하는 존
    [SerializeField] private UIArrowButton blockPaletteUI;              //블록 색입히게 설정하는 존
    [SerializeField] private UIArrowButton blockInteractionPaletteUI;   //블록 상호작용 설정하는 존

    [Header("UIScrollView")]
    [SerializeField] private UIScrollView blockBaseColorScrollView;     //블록 색입히는 스크롤 뷰
    [SerializeField] private UIScrollView blockMoveScrollView;          //블록의 움직임을 입히는 스크롤 뷰
    [SerializeField] private UIScrollView blockInteractionScrollView;   //블록에 상호작용을 입히는 스크롤 뷰

    public override void Init()
    {
        base.Init();

        saveDataUI.Init();
        floorInteractionUI.Init();
        blockPaletteUI.Init();
        blockInteractionPaletteUI.Init();
        
        blockBaseColorScrollView.CreateItem<BlockColorType>(CustomClickEvent);
        blockMoveScrollView.CreateItem<BlockMoveType>(CustomClickEvent);
        blockInteractionScrollView.CreateItem<BlockInteractionType>(CustomClickEvent);

        MapEditorManager.Instance.MapData.ShowUpFloorEvent += OnSeeUpFloor;
        MapEditorManager.Instance.MapData.HideCurFloorEvent += OnHideCurFloor;

    }

    /// <summary>
    /// 스크롤 뷰에 버튼 입력 시 실행하는 이벤트 함수
    /// </summary>
    public void CustomClickEvent(object type)
    {
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
    /// 위층 버튼 입력 함수
    /// </summary>
    public void SeeUpFloorBtn()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        MapEditorManager.Instance.MapData.ShowUpFloor();
    }

    /// <summary>
    /// 아래층 버튼 입력 함수
    /// </summary>
    public void HideCurFloorBtn()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        MapEditorManager.Instance.MapData.HideCurFloor();
    }

    /// <summary>
    /// 저장 버튼
    /// </summary>
    public void Save()
    {
        if (!MapEditorManager.Instance.CanSave())
            return;

        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CreateUI(UIType.FileBrowserPopup, false);

        string initialFilename = "SaveData_" + DateTime.Now.ToString(("MM_dd_HH_mm_ss")) + ".json";

        StartCoroutine(ShowSaveDialogCoroutine(initialFilename));
    }

    /// <summary>
    /// 로드 버튼
    /// </summary>
    public void Load()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CreateUI(UIType.FileBrowserPopup, false);

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    /// <summary>
    /// save버튼이 누르면 파일로 만들어주는 코루틴
    /// </summary>
    IEnumerator ShowSaveDialogCoroutine(string initialFilename)
    {
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.FilesAndFolders, false, null, initialFilename, "Save Files and Folders", "Save");

        if (FileBrowser.Success)
        {
            string path = FileBrowser.Result[0];

            string name = Path.GetFileNameWithoutExtension(path);           //파일명만 따오는 함수
            string json = MapEditorManager.Instance.DataToJson(name);

            System.IO.File.WriteAllText(path, json);
        }
    }

    /// <summary>
    /// load버튼이 누르면 파일 로드해주는 코루틴
    /// </summary>
    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        if (FileBrowser.Success)
        {
            MapEditorManager.Instance.LoadData(FileBrowser.Result[0]);
        }
    }

    /// <summary>
    /// 3인칭 뷰로 보여주는 함수
    /// </summary>
    public void SeeMapLookUI()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        Managers.UI.CreateUI(UIType.MapLookUI);
    }

    public override void Close()
    {
        MapEditorManager.Instance.MapData.ShowUpFloorEvent -= OnSeeUpFloor;
        MapEditorManager.Instance.MapData.HideCurFloorEvent -= OnHideCurFloor;

        base.Close();
    }

    /// <summary>
    /// 타이틀 씬으로 돌아가는 버튼 함수
    /// </summary>
    public void BackBtn()
    {
        Close(SceneType.Title);
    }
}
