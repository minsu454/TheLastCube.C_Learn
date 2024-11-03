using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIScreenView : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;

    public void CreateItem<T>() where T : Enum
    {
        foreach (T type in Enum.GetValues(typeof(T)))
        {
            GameObject go = Instantiate(slotPrefab, transform);
            UIScreenViewButton btn = go.GetComponent<UIScreenViewButton>();
            btn.Init(type);

            btn.OnClickEvent += CustomClickEvent;
        }
    }

    public void CustomClickEvent(Enum type)
    {
        MapEditorManager.Instance.SetMaterial(type);
    }
}
