using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorUI : BaseUI
{
    public Button BackButton;
    public Button StartButton;

    public void MapEditorClose()
    {
        Close(SceneType.InGame);
    }
}
