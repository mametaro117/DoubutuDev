using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    #region Singleton

    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (AudioManager)FindObjectOfType(typeof(AudioManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(AudioManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton


    [SerializeField]
    AudioClip[] BGMList = new AudioClip[3];

    [SerializeField]
    AudioClip[] SeList = new AudioClip[5];

    private AudioSource audioSource;

    [SerializeField]
    AudioMixer bgmMixer;

    void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBgm(0);
    }
    public void PlayBgm(int BGM_num)
    {
        audioSource.clip = BGMList[BGM_num];
        audioSource.Play();
    }

    public void PlaySe(int SEnum)
    {
        audioSource.PlayOneShot(SeList[SEnum]);
    }
    public void PlaySe(int SEnum, float vol = 1.0f)
    {
        audioSource.PlayOneShot(SeList[SEnum], vol);
    }

    public void FadeOutBGM()
    {
        StartCoroutine(FadeOutBgm(1));
    }

    public void FadeInBGM()
    {
        StartCoroutine(FadeOutBgm(1));
    }


    IEnumerator FadeOutBgm(float interval)
    {
        //だんだん小さく
        float time = 0;
        while (time <= interval)
        {
            bgmMixer.SetFloat("BGM", Mathf.Lerp(0, -40, time / interval));
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        yield break;
    }
    IEnumerator FadeInBgm(float interval)
    {
        //だんだん小さく
        float time = 0;
        while (time <= interval)
        {
            bgmMixer.SetFloat("BGM", Mathf.Lerp(-40, 0, time / interval));
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        yield break;
    }
}
