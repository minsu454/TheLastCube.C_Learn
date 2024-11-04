using System;
using UnityEngine;
using UnityEngine.Windows;

public class ChoiceFloorPopup : BasePopup
{
    [SerializeField] private UIInputField inputField;
    public event Action<int> ReturnValueEvent;

    public override void Init()
    {
        base.Init();
        inputField.Init();

        inputField.SetValueEvent += OnSetValue;
        inputField.SetValuecompleteEvent += OnCompleted;
    }

    private int OnSetValue(string s)
    {
        int value = int.Parse(s) - 1;

        if (value < 0 || value + MapEditorManager.Instance.MapData.ReturnCurFloor() >= 10)
            return -1;

        return value + 1;
    }

    private void OnCompleted()
    {
        int value = inputField.Value;
        if (value == -1)
            return;

        ReturnValueEvent?.Invoke(value);

        Close();
    }

    public override void Close()
    {

        base.Close();
    }
}
