using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class UIScrollViewButton : MonoBehaviour
{
    private object key;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    public event Action<object> OnClickEvent;
    
    public void Init(object key)
    {
        this.key = key;
        text1.text = key.ToString();
    }

    public void Init(object key, string str)
    {
        this.key = key;

        text1.text = key.ToString();
        text2.text = str;
    }

    public void OnButton()
    {
        Managers.Sound.PlaySFX(SfxType.UIButton);
        OnClickEvent?.Invoke(key);
    }
}
