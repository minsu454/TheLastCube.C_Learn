using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapEditorUI : MonoBehaviour
{
    [SerializeField] private UIInputField inputFieldx;
    [SerializeField] private UIInputField inputFieldy;

    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        InitInputField();
    }

    #region InputField
    public void InitInputField()
    {
        inputFieldx.SetValueEvent += OnSetInputField;
        inputFieldy.SetValueEvent += OnSetInputField;
        inputFieldx.SetValuecompleteEvent += OnInputFieldValueCompleted;
        inputFieldy.SetValuecompleteEvent += OnInputFieldValueCompleted;
    }

    public int OnSetInputField(string s)
    {
        int value = int.Parse(s);

        if (value < 0)
            value = Mathf.Max(0, value);
        else if (value > MapEditorManager.Instance.MapData.limitXZScale)
            value = Mathf.Min(MapEditorManager.Instance.MapData.limitXZScale, value);

        return value;
    }

    public void OnInputFieldValueCompleted()
    {
        MapEditorManager.Instance.MapData.SetMapScale(inputFieldx.Value, inputFieldy.Value);
    }
    #endregion


    public void Close()
    {

    }

    public void OnDestroy()
    {
        Close();
    }
}
