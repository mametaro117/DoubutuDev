using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    AudioClip[]SeList = new AudioClip[5];

    AudioSource audioSource;

    [SerializeField]
    AudioClip BgmLoopAudioClip;

    AudioSource loopAudioSours;
    
    
	void Start () {
        audioSource = GetComponent<AudioSource>();

        loopAudioSours.clip = BgmLoopAudioClip;
        loopAudioSours.loop = true;
	}

    Vector3 vec3;
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            
            PlaySe(1);
        }
    }
    public void PlaySe (int SEnum)
    {
        Debug.Log("再生");
        audioSource.clip = SeList[SEnum];
        audioSource.Play();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            PlaySe(0);
        }
        if(collision.transform.tag == "Player")
        {
            PlaySe(1);
        }
    }

    public void PlayBgm()
    {
        if(loopAudioSours == null)
        {
            return;
        }
    }
    public void StopBgm()
    {
        if (loopAudioSours == null)
        {
            return;
        }
    }

    public void Volume()
    {
       // if()
    }
}
