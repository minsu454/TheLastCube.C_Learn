using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using Common.Scene;
using Common.Event;

public class MapChoicePopup : BasePopup
{
    [Header("UIScrollView")]
    [SerializeField] private UIScrollView mapScrollView;

    private string path = $"{Application.streamingAssetsPath}/MapData";

    public override void Init()
    {
        base.Init();

        string[] fileNameArr = Directory.EnumerateFiles(path, "*.json")
                                .Select(file => Path.GetFileNameWithoutExtension(file))
                                .ToArray();

        string[] clearArr = new string[fileNameArr.Length];
        Array.Fill(clearArr, "");

        mapScrollView.CreateItem(fileNameArr, clearArr, CustomClickEvent);
    }

    public void CustomClickEvent(object type)
    {
        EventManager.Dispatch(GameEventType.StageChoice, type);

        Close(SceneType.InGame);
    }

    public void MapEditorBtn()
    {
        Close(SceneType.MapEditor);
    }
}
