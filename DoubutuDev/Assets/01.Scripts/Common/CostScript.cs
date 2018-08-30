﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostScript : MonoBehaviour {

    //  コストの最大値
    private const float MaxAnimalCost = 900;
    private const float MaxWeaponCost = 900;

    //  コスト初期値
    private float DefaultAnimalCost = 100;
    private float DefaultWeaponCost = 100;

    //  1秒あたりのコスト上昇値
    private float AddAnimalCost = 50;
    private float AddWeaponCost = 50;

    //  コストバーの最大幅取得
    private Vector2 BarRect;

    //  コスト表示のText
    [SerializeField]
    private GameObject AnimalCostTxt,WeaponCostText;
    //  実際の値
    private float AnimalCostPoint;
    private float WeaponCostPoint;
    //  幅を変更するための参照
    [SerializeField]
    private GameObject MainAnimalrectObj, MainWeaponrectObj;
    [SerializeField]
    private GameObject SubAnimalrectObj, SubWeaponrectObj;
    private RectTransform MainAnimalRect, MainWeaponRect;
    private RectTransform SubAnimalRect, SubWeaponRect;
    //  選択状態のオブジェクト参照用
    private GameObject AnimalObj = null, WeaponObj = null;
    //コルーチン発動用
    private int Retention_AnimalCost;
    private int Retention_WeaponCost;
    private int Time_AnimalCost;
    private int Time_WeaponCost;
    private int Target_AnimalCost;
    private int Target_WeaponCost;
    private float Diff_AnimalCost;
    private float Diff_WeaponCost;
    float Cor_CostPoint_AnimalCost;
    float Cor_CostPoint_WeaponCost;
    private int CorTime = 10;
    private bool isCorRunning_AnimalCost = false;
    private bool isCorRunning_WeaponCost = false;
    //ここのタイムは(処理の都合上)3の倍数にすること
    private int _Time = 9;
    private float ShakeSpeed = 0.8f;

    void Start () {
        //  バーのサイズ設定
        BarRect = MainAnimalrectObj.GetComponent<RectTransform>().sizeDelta;
        //  コストの初期化
        AnimalCostPoint = DefaultAnimalCost;
        WeaponCostPoint = DefaultWeaponCost;
        //  warning回避用にGameObjectからGetComponent
        MainAnimalRect = MainAnimalrectObj.GetComponent<RectTransform>();
        MainWeaponRect = MainWeaponrectObj.GetComponent<RectTransform>();
        SubAnimalRect = SubAnimalrectObj.GetComponent<RectTransform>();
        SubWeaponRect = SubWeaponrectObj.GetComponent<RectTransform>();
        // コルーチン誤動作防止
        Retention_AnimalCost = (int)Mathf.Floor(AnimalCostPoint / 100);
        Retention_WeaponCost = (int)Mathf.Floor(WeaponCostPoint / 100);
        //コストバー初期化
        MainAnimalRect.sizeDelta = new Vector2(BarRect.x * (AnimalCostPoint / MaxAnimalCost), MainAnimalRect.sizeDelta.y);
        MainWeaponRect.sizeDelta = new Vector2(BarRect.x * (WeaponCostPoint / MaxAnimalCost), MainWeaponRect.sizeDelta.y);
    }
	
	void Update () {
        //  コストが最大じゃなかったら加算
        if (AnimalCostPoint < MaxAnimalCost) AnimalCostPoint += Time.deltaTime * AddAnimalCost;
        if (WeaponCostPoint < MaxWeaponCost) WeaponCostPoint += Time.deltaTime * AddWeaponCost;
        CostFixed();
    }

    //  消費するコストの決定 動物
    public void SetAnimalObj(GameObject animal)
    {
        AnimalObj = animal;
    }
    //  消費するコストの決定 武器
    public void SetWeaponObj(GameObject weapon)
    {
        WeaponObj = weapon;
    }
    //  動物のコストが変化したら武器のコストを消費関数が実行できないように
    public void DeleteWeaponCost()
    {
        WeaponObj = null;
    }

    //  動物コストを消費する
    public void ConsumeAnimalCost()
    {
        float cost = AnimalObj.GetComponent<AnimalButtonScript>().GetCost() * 100;
        //  消費コストが現在のコストより小さければ
        if (cost < AnimalCostPoint)
            AnimalCostPoint -= cost;
        else
            Debug.LogWarning("動物コストが足りない");
    }

    //  武器コストを消費する
    public void ConsumeWeaponCost()
    {
        float cost = WeaponObj.GetComponent<WeaponButtonScript>().GetCost() * 100;
        //  消費コストが現在のコストより小さければ
        if (cost < WeaponCostPoint)
            WeaponCostPoint -= cost;
        else
            Debug.LogWarning("武器コストが足りない");
    }

    //  コストの値がセットされているか
    public bool IsSetCostValue()
    {
        if (AnimalObj != null && WeaponObj != null)
            return true;
        else
            return false; 
    }

    //  コストが足りているか
    public bool IsCreate()
    {
        if (AnimalCostPoint >= AnimalObj.GetComponent<AnimalButtonScript>().GetCost() * 100 && 
            WeaponCostPoint >= WeaponObj.GetComponent<WeaponButtonScript>().GetCost() * 100)
            return true;
        else
            return false;
    }

    //  どの動物を選んでいるか
    public int GetSelectNumber()
    {
        int unitNum;
        unitNum = AnimalObj.GetComponent<AnimalButtonScript>().GetSelectNum() * 3 + WeaponObj.GetComponent<WeaponButtonScript>().GetSelectNum();
        return unitNum;
    }

    // 値をコストバーに反映
    void CostFixed()
    {
        int AnimalCostInt = (int)Mathf.Floor(AnimalCostPoint / 100);
        int WeaponCostInt = (int)Mathf.Floor(WeaponCostPoint / 100);

        AnimalCostTxt.GetComponent<Text>().text = AnimalCostInt.ToString();
        WeaponCostText.GetComponent<Text>().text = WeaponCostInt.ToString();

        if(AnimalCostInt != Retention_AnimalCost)
        {
            if(isCorRunning_AnimalCost)
            {
                Debug.Log("AnimalCostコルーチンの内容を変更しました");
                Time_AnimalCost = CorTime;
                Diff_AnimalCost = (AnimalCostInt - Cor_CostPoint_AnimalCost) / Time_AnimalCost;
                Debug.Log(Diff_AnimalCost);
            }
            else
            {
                StartCoroutine(Cor_AnimalCost(AnimalCostInt - Retention_AnimalCost, Retention_AnimalCost));
            }
            Retention_AnimalCost = AnimalCostInt;
        }

        if (WeaponCostInt != Retention_WeaponCost)
        {
            if (isCorRunning_WeaponCost)
            {
                Debug.Log("WeaponCostコルーチンの内容を変更しました");
                Time_WeaponCost = CorTime;
                Diff_WeaponCost = (WeaponCostInt - Cor_CostPoint_WeaponCost) / Time_WeaponCost;
                Debug.Log(Diff_WeaponCost);
            }
            else
            {
                StartCoroutine(Cor_WeaponCost(WeaponCostInt - Retention_WeaponCost, Retention_WeaponCost));
            }
            Retention_WeaponCost = WeaponCostInt;
        }

        SubAnimalRect.sizeDelta = new Vector2(BarRect.x * (AnimalCostPoint / MaxAnimalCost), SubAnimalRect.sizeDelta.y);
        SubWeaponRect.sizeDelta = new Vector2(BarRect.x * (WeaponCostPoint / MaxWeaponCost), SubWeaponRect.sizeDelta.y);
    }

    //Mainのコストバーを変化させるコルーチン
    //for動物
    //コストバーを指定コストまで持っていくコルーチン
    IEnumerator Cor_AnimalCost(int Change, int OriginAnimalCost)
    {
        Debug.Log("StartCol_AnimalCost");
        isCorRunning_AnimalCost = true;
        Time_AnimalCost = CorTime;
        Diff_AnimalCost = (float)Change / Time_AnimalCost;
        Cor_CostPoint_AnimalCost = OriginAnimalCost;

        //指定の所までもっていく
        while(Time_AnimalCost >= 0)
        {
            Time_AnimalCost--;
            Cor_CostPoint_AnimalCost += Diff_AnimalCost;
            //Debug.Log(Cor_CostPoint_AnimalCost);
            MainAnimalRect.sizeDelta = new Vector2(BarRect.x * (Cor_CostPoint_AnimalCost * 100 / MaxAnimalCost), MainAnimalRect.sizeDelta.y);
            yield return null;
        }
        StartCoroutine(Cor_ShakeAnimalCost(Diff_AnimalCost));
        isCorRunning_AnimalCost = false;
        Debug.Log("EndCol_AnimalCost");
        yield break;
    }
    //コストバーを振るコルーチン
    IEnumerator Cor_ShakeAnimalCost(float speed)
    {
        Debug.Log("StartCol_AnimalShake");
        int Time_ShakeAnimalCost = _Time;
        int TotalTime = _Time;
        while(Time_ShakeAnimalCost > 0)
        {
            Time_ShakeAnimalCost--;
            // 1/3フレーム目までは通り過ぎさせる
            if(Time_ShakeAnimalCost >= TotalTime / 3 * 2)
            {
                Cor_CostPoint_AnimalCost += speed * ShakeSpeed;
                MainAnimalRect.sizeDelta = new Vector2(BarRect.x * (Cor_CostPoint_AnimalCost * 100 / MaxAnimalCost), MainAnimalRect.sizeDelta.y);
            }
            // 1/3フレーム目から2/3フレーム目までは少し戻る(1.5倍速)
            else if(Time_ShakeAnimalCost >= TotalTime / 3)
            {
                Cor_CostPoint_AnimalCost += speed * -1.5f * ShakeSpeed;
                MainAnimalRect.sizeDelta = new Vector2(BarRect.x * (Cor_CostPoint_AnimalCost * 100 / MaxAnimalCost), MainAnimalRect.sizeDelta.y);
            }
            // 2/3フレーム目からは元に戻る(0.5倍速)
            else
            {
                Cor_CostPoint_AnimalCost += speed * 0.5f * ShakeSpeed;
                MainAnimalRect.sizeDelta = new Vector2(BarRect.x * (Cor_CostPoint_AnimalCost * 100 / MaxAnimalCost), MainAnimalRect.sizeDelta.y);
            }
            yield return null; 
        }
        Debug.Log("EndCol_AnimalShake");
        yield break;
    }

    //for武器
    //コストバーを指定コストまで持っていくコルーチン
    IEnumerator Cor_WeaponCost(int Change, int OriginWeaponCost)
    {
        Debug.Log("StartCol_WeaponCost");
        isCorRunning_WeaponCost = true;
        Time_WeaponCost = CorTime;
        Diff_WeaponCost = (float)Change / Time_WeaponCost;
        Cor_CostPoint_WeaponCost = OriginWeaponCost;

        //指定の所までもっていく
        while (Time_WeaponCost >= 0)
        {
            Time_WeaponCost--;
            Cor_CostPoint_WeaponCost += Diff_WeaponCost;
            //Debug.Log(Cor_CostPoint_WeaponCost);
            MainWeaponRect.sizeDelta = new Vector2(BarRect.x * (Cor_CostPoint_WeaponCost * 100 / MaxWeaponCost), MainWeaponRect.sizeDelta.y);
            yield return null;
        }
        StartCoroutine(Cor_ShakeWeaponCost(Diff_WeaponCost));
        isCorRunning_WeaponCost = false;
        Debug.Log("EndCol_WeaponCost");
        yield break;
    }
    //コストバーを振るコルーチン
    IEnumerator Cor_ShakeWeaponCost(float speed)
    {
        Debug.Log("StartCol_WeaponShake");
        int Time_ShakeWeaponCost = _Time;
        int TotalTime = _Time;
        while (Time_ShakeWeaponCost > 0)
        {
            Time_ShakeWeaponCost--;
            // 1/3フレーム目までは通り過ぎさせる
            if (Time_ShakeWeaponCost >= TotalTime / 3 * 2)
            {
                Cor_CostPoint_WeaponCost += speed * ShakeSpeed;
                MainWeaponRect.sizeDelta = new Vector2(BarRect.x * (Cor_CostPoint_WeaponCost * 100 / MaxWeaponCost), MainWeaponRect.sizeDelta.y);
            }
            // 1/3フレーム目から2/3フレーム目までは少し戻る(1.5倍速)
            else if (Time_ShakeWeaponCost >= TotalTime / 3)
            {
                Cor_CostPoint_WeaponCost += speed * -1.5f * ShakeSpeed;
                MainWeaponRect.sizeDelta = new Vector2(BarRect.x * (Cor_CostPoint_WeaponCost * 100 / MaxWeaponCost), MainWeaponRect.sizeDelta.y);
            }
            // 2/3フレーム目からは元に戻る(0.5倍速)
            else
            {
                Cor_CostPoint_WeaponCost += speed * 0.5f * ShakeSpeed;
                MainWeaponRect.sizeDelta = new Vector2(BarRect.x * (Cor_CostPoint_WeaponCost * 100 / MaxWeaponCost), MainWeaponRect.sizeDelta.y);
            }
            yield return null;
        }
        Debug.Log("EndCol_WeaponShake");
        yield break;
    }
}
