using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLookUI : BasePopup
{
    public override void Init()
    {
        base.Init();

        MapEditorManager.Instance.MapEditorGo.SetActive(false);
        MapEditorManager.Instance.MapLookGo.SetActive(true);
    }

    public override void Close()
    {
        MapEditorManager.Instance.MapLookGo.SetActive(false);
        MapEditorManager.Instance.MapEditorGo.SetActive(true);
            
        base.Close();
    }
}
