using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        Managers.UI.CreateUI(UIType.StartSceneUI);
    }
}
