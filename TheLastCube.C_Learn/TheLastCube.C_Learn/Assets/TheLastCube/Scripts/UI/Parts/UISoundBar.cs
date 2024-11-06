using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundBar : MonoBehaviour
{
    [SerializeField] private Scrollbar bgmScrollbar;
    [SerializeField] private Scrollbar sfxScrollbar;

    public void Init()
    {
        bgmScrollbar.value = PlayerPrefs.GetFloat("BGM", 1);
        sfxScrollbar.value = PlayerPrefs.GetFloat("SFX", 1);
    }

    public void OnBGMScrollbar()
    {
        Managers.Sound.SetVolume("BGM", bgmScrollbar.value);
    }

    public void OnSFXScrollbar()
    {
        Managers.Sound.SetVolume("SFX", sfxScrollbar.value);
    }
}
