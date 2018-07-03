using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    AudioClip[] SeList = new AudioClip[5];

    AudioSource audioSource;


    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySe(int SEnum)
    {
        //audioSource.clip = SeList[SEnum];
        audioSource.PlayOneShot(SeList[SEnum]);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            PlaySe(0);
        }

        if (collision.transform.tag == "Player")
        {
            PlaySe(1);
        }
    }

    public void OnMouseEnter()
    {
        PlaySe(0);
        PlaySe(1);
    }
}
