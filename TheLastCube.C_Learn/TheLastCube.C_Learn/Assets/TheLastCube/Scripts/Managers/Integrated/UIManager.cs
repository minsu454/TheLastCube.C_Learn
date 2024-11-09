using Common.Scene;
using Core.StringExtensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour, IManager
{
    private readonly Dictionary<Enum, GameObject> UIContainerDic = new Dictionary<Enum, GameObject>();      //모든 UI 담아두는 Dictionary
    private readonly Stack<BasePopup> depth = new Stack<BasePopup>();
    //UI를 열게될 때마다 Stack에 추가됨.

    public void Init()
    {
        CreateDic<SceneType>("Prefabs/UI/Main");
        CreateDic<UIType>("Prefabs/UI/Popup");

        SceneManagerEx.OnLoadCompleted(SetBaseUI);
    }

    /// <summary>
    /// container에 값 넣어주는 함수
    /// </summary>
    private void CreateDic<T>(string path) where T : Enum
    {
        foreach (T type in Enum.GetValues(typeof(T)))
        {
            GameObject go = Resources.Load<GameObject>(string.Format($"{path}/{type.ToString()}"));

            if (go == null)
                continue;

            UIContainerDic.Add(type, go);
        }
    }

    /// <summary>
    /// 씬에 기본 UI깔아주는 함수
    /// </summary>
    public void SetBaseUI(Scene scene, LoadSceneMode mode)
    {
        depth.Clear();

        SceneType type = StringExtensions.StringToEnum<SceneType>(scene.name);
        CreateUI(type);
    }

    /// <summary>
    /// UI생성해주는 함수
    /// </summary>
    public BasePopup CreateUI(Enum type, bool curPopupActive = true)
    {
        if (!UIContainerDic.TryGetValue(type, out GameObject prefab))
        {
            Debug.LogWarning($"Is Not Scene base UI : {type}");
            return null;
        }

        GameObject clone = Instantiate(prefab);

        if (depth.TryPeek(out BasePopup beforeUI) && curPopupActive)
        {
            beforeUI.gameObject.SetActive(false);
        }

        BasePopup afterUI = clone.GetComponent<BasePopup>();
        afterUI.Init();

        depth.Push(afterUI);

        return afterUI;
    }

    /// <summary>
    /// UI닫아주는 함수
    /// </summary>
    public void CloseUI(Action LoadScene = null)
    {
        if (LoadScene != null)
        {
            depth.Clear();
            LoadScene();
            return;
        }

        if (depth.Count == 1)
        {
            return;
        }

        Destroy(depth.Pop().gameObject);

        if (depth.TryPeek(out BasePopup baseUI))
        {
            baseUI.gameObject.SetActive(true);
        }
    }

}
