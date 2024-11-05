using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class UIScrollViewButton : MonoBehaviour
{
    private object enumType;
    [SerializeField] private TextMeshProUGUI text;
    public event Action<object> OnClickEvent;
    
    public void Init(object enumType)
    {
        this.enumType = enumType;
        text.text = enumType.ToString();
    }

    public void OnButton()
    {
        OnClickEvent?.Invoke(enumType);
    }
}
