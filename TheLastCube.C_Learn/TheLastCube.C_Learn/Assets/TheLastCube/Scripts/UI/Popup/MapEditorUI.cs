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
    [SerializeField] private GameObject NotSeeMapUI;

    [SerializeField] private TMP_Text floorText;

    [Header("UIArrow")]
    [SerializeField] private UIArrowButton saveDataUI;
    [SerializeField] private UIArrowButton floorInteractionUI;
    [SerializeField] private UIArrowButton blockPaletteUI;
    [SerializeField] private UIArrowButton blockInteractionPaletteUI;

    [Header("UIScrollView")]
    [SerializeField] private UIScrollView blockBaseColorScrollView;
    [SerializeField] private UIScrollView blockMoveScrollView;
    [SerializeField] private UIScrollView blockInteractionScrollView;

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
    public void SeeUpFloorBtn()
    {
        MapEditorManager.Instance.MapData.ShowUpFloor();
    }

    public void HideCurFloorBtn()
    {
        MapEditorManager.Instance.MapData.HideCurFloor();
    }

    /// <summary>
    /// 저장 버튼
    /// </summary>
    public void Save()
    {
        if (!MapEditorManager.Instance.CanSave())
            return;

        Managers.UI.CreateUI(UIType.FileBrowserPopup, false);

        string initialFilename = "SaveData_" + DateTime.Now.ToString(("MM_dd_HH_mm_ss")) + ".json";

        StartCoroutine(ShowSaveDialogCoroutine(initialFilename));
    }

    /// <summary>
    /// 로드 버튼
    /// </summary>
    public void Load()
    {
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
