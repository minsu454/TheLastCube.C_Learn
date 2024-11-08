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
    [SerializeField] private int value = 0;                 //인풋필드에 입력된 값
    public int Value
    {
        get { return value; }
    }

    public event Func<string, int> SetValueEvent;           //값 설정 조건을 주는 이벤트
    public event Action SetValuecompleteEvent;              //값 설정이 끝났을 때 실행해주는 이벤트

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(OnInputField);
    }

    /// <summary>
    /// inputField값이 바뀔 때 호출하는 함수
    /// </summary>
    private void OnInputField(string s)
    {
        if (SetValueEvent == null)
            return;

        value = SetValueEvent.Invoke(s);
        SetValuecompleteEvent?.Invoke();
    }
}
