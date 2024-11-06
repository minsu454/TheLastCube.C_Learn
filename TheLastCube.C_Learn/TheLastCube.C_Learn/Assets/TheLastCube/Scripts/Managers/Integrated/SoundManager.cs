using Common.Scene;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SoundManager : MonoBehaviour, IManager
{
    public AudioSource sfxSource;       //sfx전용 오디오 변수
    public AudioSource bgmSource;       //bgm전용 오디오 변수

    private Dictionary<Enum, AudioClip> clipDic = new Dictionary<Enum, AudioClip>();       //sfx클립 저장해놓는 Dic

    /// <summary>
    /// SFX 재생 함수
    /// </summary>
    public void PlaySFX(SfxType type)
    {
        sfxSource.PlayOneShot(clipDic[type]);
    }

    /// <summary>
    /// BGM 재생 함수
    /// </summary>
    public void PlayBGM(BgmType type)
    {
        if (bgmSource.clip == clipDic[type])
            return;

        bgmSource.clip = clipDic[type];
        bgmSource.Play();
    }

    /// <summary>
    /// 씬 로드 시 호출해주는 함수
    /// </summary>
    public void OnLoadCompleted(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch (scene.name)
        {
            case "Title":
                if (bgmSource.clip == null || bgmSource.clip != clipDic[BgmType.Title])
                {
                    bgmSource.clip = clipDic[BgmType.Title];
                    bgmSource.Play();
                }
                break;
            case "InGame":
                if (bgmSource.clip == null || bgmSource.clip != clipDic[BgmType.InGame])
                {
                    bgmSource.clip = clipDic[BgmType.InGame];
                    bgmSource.Play();
                }
                break;
        }
    }

    /// <summary>
    /// SoundManager 생성 함수
    /// </summary>
    public void Init()
    {
        GameObject sfxGo = new GameObject("SFX");
        GameObject bgmGo = new GameObject("BGM");

        sfxGo.transform.SetParent(transform);
        bgmGo.transform.SetParent(transform);

        sfxSource = sfxGo.AddComponent<AudioSource>();
        bgmSource = bgmGo.AddComponent<AudioSource>();

        var sfxClipArr = Resources.LoadAll<AudioClip>("Sounds/SFX");
        ClipLoader<SfxType>(sfxClipArr);

        var bgmClipArr = Resources.LoadAll<AudioClip>("Sounds/BGM");
        ClipLoader<BgmType>(bgmClipArr);

        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
        bgmSource.volume = 0.3f;

        sfxSource.playOnAwake = false;
        sfxSource.volume = 0.3f;    

        SceneManagerEx.OnLoadCompleted(OnLoadCompleted);
    }

    /// <summary>
    /// 클립 읽어주는 함수
    /// </summary>
    public void ClipLoader<T>(AudioClip[] arr) where T : Enum
    {
        for (int i = 0; i < arr.Length; i++)
        {
            try
            {
                T type = (T)Enum.Parse(typeof(T), arr[i].name);
                clipDic.Add(type, arr[i]);
            }
            catch
            {
                Debug.LogWarning("Need Enum : " + arr[i].name);
            }
        }
    }


}
