using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class UIScreenViewButton : MonoBehaviour
{
    private Enum enumType;
    [SerializeField] private TextMeshProUGUI text;
    public event Action<Enum> OnClickEvent;
    
    public void Init(Enum enumType)
    {
        this.enumType = enumType;
        text.text = enumType.ToString();
    }

    public void OnButton()
    {
        OnClickEvent?.Invoke(enumType);
    }
}
