using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostScript : MonoBehaviour {

    //  コストの最大値
    private const float MaxAnimalCost = 500;
    private const float MaxWeaponCost = 500;
    //  コスト表示のText
    [SerializeField]
    private Text AnimalCostTxt,WeaponCostText;
    //  初期値
    [SerializeField]
    private float DefaultAnimalCost = 100;
    [SerializeField]
    private float DefaultWeaponCost = 100;
    //  実際の値
    private float AnimalCostPoint;
    private float WeaponCostPoint;
    //  幅を変更するための参照
    [SerializeField]
    private RectTransform Animalrect, Weaponrect;

    private float AnimalCost = -1, WeaponCost = -1;


    void Start () {
        //  コストの初期化
        AnimalCostPoint = DefaultAnimalCost;
        WeaponCostPoint = DefaultWeaponCost;
    }
	
	void Update () {
        //  コストが最大じゃなかったら加算
        if (AnimalCostPoint < MaxAnimalCost) AnimalCostPoint += Time.deltaTime * 25;
        if (WeaponCostPoint < MaxWeaponCost) WeaponCostPoint += Time.deltaTime * 25;
        CostFixed();
    }

    //  消費するコストの決定 動物
    public void SetAnimalCost(float num)
    {
        AnimalCost = num;
    }
    //  消費するコストの決定 武器
    public void SetWeaponCost(float num)
    {
        WeaponCost = num;
    }
    //  動物のコストが変化したら武器のコストを消費関数が実行できないように負の値にへ
    public void DeleteWeaponCost()
    {
        WeaponCost = -1;
    }

    //  動物コストを消費する
    public void ConsumeAnimalCost()
    {
        float cost = AnimalCost * 100;
        //  消費コストが現在のコストより小さければ
        if (cost < AnimalCostPoint)
            AnimalCostPoint -= cost;
        else
            Debug.LogWarning("動物コストが足りない");
    }

    //  武器コストを消費する
    public void ConsumeWeaponCost()
    {
        float cost = WeaponCost * 100;
        //  消費コストが現在のコストより小さければ
        if (cost < WeaponCostPoint)
            WeaponCostPoint -= cost;
        else
            Debug.LogWarning("武器コストが足りない");
    }

    //  コストの値がセットされているか
    public bool IsSetCostValue()
    {
        if (AnimalCost >= 1 && WeaponCost >= 1)
            return true;
        else
            return false; 
    }

    /// <summary>
    /// コストが足りているか
    /// </summary>
    public bool IsCreate()
    {
        if (AnimalCostPoint >= AnimalCost * 100 && WeaponCostPoint >= WeaponCost * 100)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 値をコストバーに反映
    /// </summary>
    void CostFixed()
    {
        AnimalCostTxt.text = Mathf.Floor(AnimalCostPoint / 100).ToString("F0");
        WeaponCostText.text = Mathf.Floor(WeaponCostPoint / 100).ToString("F0");
        Animalrect.sizeDelta = new Vector2(AnimalCostPoint / MaxAnimalCost * MaxAnimalCost, Animalrect.sizeDelta.y);
        Weaponrect.sizeDelta = new Vector2(WeaponCostPoint / MaxWeaponCost * MaxWeaponCost, Weaponrect.sizeDelta.y);
    }
}
