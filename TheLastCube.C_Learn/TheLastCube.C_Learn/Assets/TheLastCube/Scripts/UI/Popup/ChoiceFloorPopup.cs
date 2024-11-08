using System;
using UnityEngine;
using UnityEngine.Windows;

public class ChoiceFloorPopup : BasePopup
{
    [SerializeField] private UIInputField inputField;   //층수 입력필드
    public event Action<int> ReturnValueEvent;

    public override void Init()
    {
        base.Init();
        inputField.Init();

        inputField.SetValueEvent += OnSetValue;
        inputField.SetValuecompleteEvent += OnCompleted;
    }

    /// <summary>
    /// inputField 값 설정 조건을 주는 함수
    /// </summary>
    private int OnSetValue(string s)
    {
        int value = int.Parse(s) - 1;

        if (value < 0 || value + MapEditorManager.Instance.MapData.ReturnCurFloor() >= 10)
            return -1;

        return value + 1;
    }

    /// <summary>
    /// inputField 값 설정이 끝났을 때 실행해주는 함수
    /// </summary>
    private void OnCompleted()
    {
        int value = inputField.Value;
        if (value == -1)
            return;

        ReturnValueEvent?.Invoke(value);

        Close();
    }
}
