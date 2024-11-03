using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLookUI : BasePopup
{
    public override void Init()
    {
        base.Init();

        MapEditorManager.Instance.MapEditorController.SetActive(false);
        MapEditorManager.Instance.MapLookController.SetActive(true);
    }

    public override void Close()
    {
        MapEditorManager.Instance.MapLookController.SetActive(false);
        MapEditorManager.Instance.MapEditorController.SetActive(true);
            
        base.Close();
    }
}
