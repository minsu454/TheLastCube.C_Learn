using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour, IManager
{

    private readonly Dictionary<UIType, GameObject> UIContainer = new Dictionary<UIType, GameObject>();
    private readonly Stack<GameObject> _stack = new Stack<GameObject>();
    //UI를 열게될 때마다 Stack에 추가됨.


    public void Init() // 생성 초기값
    {
        foreach (UIType type in Enum.GetValues(typeof(UIType)))
        {
            GameObject gameObject = Resources.Load<GameObject>($"{type.ToString()}");
            UIContainer.Add(type, gameObject);
        }
    }

    public void CreateUI(UIType type)
    {
        if (UIContainer.TryGetValue(type, out GameObject prefab))
        {
            GameObject createdGameObject = Instantiate(prefab);
            _stack.Push(createdGameObject);
        }
    }

    public void CloseUI()
    {
        if (_stack.Count > 0)
        {
            GameObject uiClose = _stack.Pop(); // 스택에서 가장 최근의 UI를 꺼냄 - 후입선출
            Destroy(uiClose); // UI 오브젝트를 파괴하여 닫음
        }
        else
        {
            Debug.Log("Close UI is not exist.");
        }
    }

}
