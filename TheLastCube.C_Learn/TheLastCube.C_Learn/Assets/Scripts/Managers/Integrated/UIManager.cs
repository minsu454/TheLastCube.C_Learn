using Common.Scene;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour, IManager
{
    private readonly Dictionary<UIType, GameObject> UIContainer = new Dictionary<UIType, GameObject>();
    private readonly Stack<BaseUI> depth = new Stack<BaseUI>();
    //UI를 열게될 때마다 Stack에 추가됨.

    public void Init() // 생성 초기값
    {
        foreach (UIType type in Enum.GetValues(typeof(UIType)))
        {
            GameObject gameObject = Resources.Load<GameObject>($"Prefabs/Popup/{type.ToString()}");
            UIContainer.Add(type, gameObject);
        }
    }

    public void CreateUI(UIType type, bool curPopupActive = true)
    {
        if (!UIContainer.TryGetValue(type, out GameObject prefab))
        {
            Debug.LogWarning($"Is Not Scene base UI : {type}");
            return;
        }

        GameObject clone = Instantiate(prefab);

        if (depth.TryPeek(out BaseUI beforeUI) && curPopupActive)
        {
            beforeUI.gameObject.SetActive(false);
        }

        BaseUI afterUI = clone.GetComponent<BaseUI>();
        afterUI.Init();

        depth.Push(beforeUI);
    }

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

        Destroy(depth.Pop());

        if (depth.TryPeek(out BaseUI baseUI))
        {
            baseUI.Init();
            baseUI.gameObject.SetActive(true);
        }
    }

}
