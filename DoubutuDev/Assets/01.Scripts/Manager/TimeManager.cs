using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    [SerializeField]
    private Text TimeText;
    [SerializeField]
    private float Timelimit;
    [SerializeField]
    private GameObject ReadyImage, GoImage, ClearImage, FailImage;

    private bool isReady = false;
    private bool isFinish = false;


    void Start()
    {
        TimeText.text = Timelimit.ToString("F0");
        StartCoroutine(ReadyGo());
    }

    void Update()
    {
        CountDown();
    }

    void CountDown()
    {
        //  カウントダウンが終わってバトルが終わってなかったら
        if (isReady && !isFinish)
        {
            if (Timelimit >= 0)
            {
                Timelimit -= Time.deltaTime;
                TimeText.text = Timelimit.ToString("F0");
            }
            Mathf.Max(0, Timelimit);
        }
    }

    public void GameFinish()
    {
        
    }




    
    
    //  開始時のカウントダウン処理
    IEnumerator ReadyGo()
    {
        Time.timeScale = 0;
        GoImage.SetActive(false);
        yield return new WaitForSecondsRealtime(1.0f);
        ReadyImage.SetActive(true);
        yield return new WaitForSecondsRealtime(2.0f);
        ReadyImage.SetActive(false);
        yield return new WaitForSecondsRealtime(1.0f);
        GoImage.SetActive(true);
        yield return new WaitForSecondsRealtime(1.0f);
        GoImage.SetActive(false);
        Time.timeScale = 1;
        isReady = true;
        yield break;
    }
}
