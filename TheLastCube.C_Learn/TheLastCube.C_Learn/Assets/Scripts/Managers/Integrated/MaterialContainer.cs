using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public sealed class MaterialContainer : MonoBehaviour, IManager
{
    private readonly Dictionary<Enum, Material> groundContainerDic = new Dictionary<Enum, Material>();
    //UI를 열게될 때마다 Stack에 추가됨.

    public void Init() // 생성 초기값
    {
        CreateDic<BlockColorType>();
        CreateDic<BlockMoveType>();
        CreateDic<BlockInteractionType>();
    }

    /// <summary>
    /// dictionary enum 값으로 만들어주는 함수
    /// </summary>
    private void CreateDic<T>() where T : Enum
    {
        string stringEnum = typeof(T).ToString();
        foreach (T type in Enum.GetValues(typeof(T)))
        {
            string stringType = type.ToString();
            string name = $"Materials/{stringEnum}/{stringType}";

            Material material = Resources.Load<Material>(name);
            groundContainerDic.Add(type, material);
        }
    }

    public Material Return(Enum type)
    {
        if (!groundContainerDic.TryGetValue(type, out Material material))
        {
            Debug.LogError($"Is Not Key Dictionary : {type}");
            return null;
        }

        return material;
    }
}