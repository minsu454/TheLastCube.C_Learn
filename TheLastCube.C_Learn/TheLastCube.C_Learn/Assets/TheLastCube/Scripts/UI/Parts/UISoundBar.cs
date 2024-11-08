using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundBar : MonoBehaviour
{
    [SerializeField] private Scrollbar bgmScrollbar;    //bgm바
    [SerializeField] private Scrollbar sfxScrollbar;    //sfx바

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init()
    {
        bgmScrollbar.value = PlayerPrefs.GetFloat("BGM", 1);
        sfxScrollbar.value = PlayerPrefs.GetFloat("SFX", 1);
    }

    /// <summary>
    /// BGM 스크롤 설정
    /// </summary>
    public void OnBGMScrollbar()
    {
        Managers.Sound.SetVolume("BGM", bgmScrollbar.value);
    }

    /// <summary>
    /// SFX 스크롤 설정
    /// </summary>
    public void OnSFXScrollbar()
    {
        Managers.Sound.SetVolume("SFX", sfxScrollbar.value);
    }
}
