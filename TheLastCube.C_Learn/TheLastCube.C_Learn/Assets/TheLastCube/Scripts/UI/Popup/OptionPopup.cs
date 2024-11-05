using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : BasePopup
{
    //bgm과 sfx에 들어갈 오디오 파일을 넣을 변수
    public AudioSource bgmAudioSource;
    public AudioSource[] sfxAudioSource;

    //bgm과 sfx 스크롤바에 들어갈 변수
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        //스크롤바 값 초기화
        bgmSlider.value = bgmAudioSource.volume;
        sfxSlider.value = sfxAudioSource[0].volume;

        bgmSlider.onValueChanged.AddListener(BgmVolume);
        sfxSlider.onValueChanged.AddListener(SfxVolume);
    }

    public void BgmVolume(float volume)
    {
        bgmAudioSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        foreach(AudioSource audioSource in sfxAudioSource)
        {
            audioSource.volume = volume;
        }
    }
}
