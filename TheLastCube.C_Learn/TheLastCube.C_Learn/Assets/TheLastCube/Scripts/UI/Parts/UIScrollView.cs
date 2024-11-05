using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIScrollView : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;

    public void CreateItem<T>(Action<object> onClickEvent = null) where T : Enum
    {
        foreach (T type in Enum.GetValues(typeof(T)))
        {
            GameObject go = Instantiate(slotPrefab, transform);
            UIScrollViewButton btn = go.GetComponent<UIScrollViewButton>();
            btn.Init(type);

            if(onClickEvent != null)
                btn.OnClickEvent += onClickEvent;
        }
    }

    public void CreateItem(string[] arr1, string[] arr2, Action<object> onClickEvent = null)
    {
        if (arr1.Length != arr2.Length)
            return;

        for (int i = 0; i < arr1.Length; i++)
        {
            GameObject go = Instantiate(slotPrefab, transform);
            UIScrollViewButton btn = go.GetComponent<UIScrollViewButton>();
            btn.Init(arr1[i], arr2[i]);

            if (onClickEvent != null)
                btn.OnClickEvent += onClickEvent;
        }
    }
}
