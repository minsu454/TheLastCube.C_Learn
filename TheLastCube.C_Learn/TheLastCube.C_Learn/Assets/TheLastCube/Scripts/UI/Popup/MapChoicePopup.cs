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
    [SerializeField] private UIScrollView mapScrollView;                    //맵 스크롤뷰

    private string path = $"{Application.streamingAssetsPath}/MapData";     //데이터 파일 위치

    public override void Init()
    {
        base.Init();

        SetFileName();
    }

    /// <summary>
    /// 파일이름만큼 스테이지 버튼 늘려주는 함수
    /// </summary>
    private void SetFileName()
    {
        string[] fileNameArr = Directory.EnumerateFiles(path, "*.json")
                                .Select(file => Path.GetFileNameWithoutExtension(file))
                                .ToArray();
        string[] clearArr = new string[fileNameArr.Length];

        for (int i = 0; i < fileNameArr.Length; i++)
        {
            if (SaveData.GetClearData(fileNameArr[i]))
            {
                clearArr[i] = "Clear";
            }
            else
            {
                clearArr[i] = "";
            }

        }

        mapScrollView.CreateItem(fileNameArr, clearArr, CustomClickEvent);
    }

    /// <summary>
    /// 버튼 클릭 이벤트
    /// </summary>
    /// <param name="type"></param>
    public void CustomClickEvent(object type)
    {
        EventManager.Dispatch(GameEventType.StageChoice, type);

        Close(SceneType.InGame);
    }

    /// <summary>
    /// 맵에디터로 이동하는 버튼
    /// </summary>
    public void MapEditorBtn()
    {
        Close(SceneType.MapEditor);
    }
}
