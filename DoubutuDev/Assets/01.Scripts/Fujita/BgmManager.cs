using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgmManager : MonoBehaviour {


    [SerializeField]
    AudioClip BgmLoopAudioClip;
    [SerializeField]
    AudioSource loopAudioSource;

    //ゲーム再生時のBGMループ再生
	void Start () {

        loopAudioSource.clip = BgmLoopAudioClip;
        loopAudioSource.loop = true;
	}

    //BGMのボリューム調整
    public void PlayBgm(float vol = 1.0f){

        if (loopAudioSource == null)
        {
            return;
        }

        loopAudioSource.volume = vol;
        loopAudioSource.Play();
    }

    //再生時の確認用
    //public void OnMouseEnter(){

    //    PlayBgm();
    //}
}
