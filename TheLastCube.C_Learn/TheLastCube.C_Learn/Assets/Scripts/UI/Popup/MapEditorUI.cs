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

    /// <summary>
    /// 저장 버튼
    /// </summary>
    public void Save()
    {
        Managers.UI.CreateUI(UIType.FileBrowserPopup, false);

        string initialFilename = "SaveData_" + DateTime.Now.ToString(("MM_dd_HH_mm_ss")) + ".json";

        StartCoroutine(ShowSaveDialogCoroutine(initialFilename));
    }

    public void SeeMapLookUI()
    {
        Managers.UI.CreateUI(UIType.MapLookUI);
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

            //System.IO.File.WriteAllText(path, json);
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
            //PattenGenerator.Instance.LoadData(FileBrowser.Result[0]);
        }
    }
}
