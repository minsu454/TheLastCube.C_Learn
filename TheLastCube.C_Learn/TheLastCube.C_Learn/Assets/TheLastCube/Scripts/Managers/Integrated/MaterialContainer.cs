using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public sealed class MaterialContainer : MonoBehaviour, IManager
{
    private readonly Dictionary<Enum, Material> materialContainerDic = new Dictionary<Enum, Material>();      //블록에 모든 Material ContainerDic
    //UI를 열게될 때마다 Stack에 추가됨.

    public void Init() // 생성 초기값
    {
        CreateDic<BlockColorType>();
        CreateDic<BlockMoveType>();
        CreateDic<BlockInteractionType>();
        CreateDic<BlockEventType>();
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
            materialContainerDic.Add(type, material);
        }
    }

    /// <summary>
    /// Material 반환 함수
    /// </summary>
    public Material Return(Enum type)
    {
        if (!materialContainerDic.TryGetValue(type, out Material material))
        {
            Debug.LogError($"Is Not Key Dictionary : {type}");
            return null;
        }

        return material;
    }
}