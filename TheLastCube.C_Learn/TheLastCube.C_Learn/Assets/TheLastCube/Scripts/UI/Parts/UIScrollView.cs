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
}
