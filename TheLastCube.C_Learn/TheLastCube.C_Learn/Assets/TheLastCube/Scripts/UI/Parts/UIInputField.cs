using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIInputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private int value = 0;
    public int Value
    {
        get { return value; }
    }

    public event Func<string, int> SetValueEvent;
    public event Action SetValuecompleteEvent;

    public void Init()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(OnInputField);
    }

    private void OnInputField(string s)
    {
        if (SetValueEvent == null)
            return;

        value = SetValueEvent.Invoke(s);
        SetValuecompleteEvent?.Invoke();
    }
}
