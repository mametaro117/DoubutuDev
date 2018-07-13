using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    AudioSource audioSource;

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
        PlayBGM(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(DownBGM(2));
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(UpBGM(2));
        }
    }

    public void PlayBGM(int BGM_num)
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


    public IEnumerator FadeBgm(float interval)
    {
        //だんだん小さく
        float time = 0;
        while (time <= interval)
        {
            Mathf.Lerp(0f, 1f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }

        //だんだん大きく
        time = 0;
        while (time <= interval)
        {
            Mathf.Lerp(0f, 1f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
    }

    IEnumerator DownBGM(float interval)
    {
        float time = 0;
        AudioSource audioSource = GetComponent<AudioSource>();
        while (time <= interval)
        {
            audioSource.volume = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        yield break;
    }

    IEnumerator UpBGM(float interval)
    {
        float time = 0;
        AudioSource audioSource = GetComponent<AudioSource>();

        while (time <= interval)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.unscaledDeltaTime;
            yield return 0;
        }
        yield break;
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Enemy")
    //    {
    //        PlaySe(0, 0.2f);
    //    }
    //    if (collision.transform.tag == "Player")
    //    {
    //        PlaySe(1, 1);
    //    }
    //}
}
