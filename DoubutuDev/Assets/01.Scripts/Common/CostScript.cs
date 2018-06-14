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
    private GameObject AnimalCostTxt,WeaponCostText;
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
    private GameObject AnimalrectObj, WeaponrectObj;
    private RectTransform AnimalRect, WeaponRect;
    //  選択状態のオブジェクト参照用
    private GameObject AnimalObj = null, WeaponObj = null;


    void Start () {
        //  コストの初期化
        AnimalCostPoint = DefaultAnimalCost;
        WeaponCostPoint = DefaultWeaponCost;
        //  warning回避用にGameObjectからGetComponent
        AnimalRect = AnimalrectObj.GetComponent<RectTransform>();
        WeaponRect = WeaponrectObj.GetComponent<RectTransform>();
    }
	
	void Update () {
        //  コストが最大じゃなかったら加算
        if (AnimalCostPoint < MaxAnimalCost) AnimalCostPoint += Time.deltaTime * 25;
        if (WeaponCostPoint < MaxWeaponCost) WeaponCostPoint += Time.deltaTime * 25;
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
    //  動物のコストが変化したら武器のコストを消費関数が実行できないように負の値にへ
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

    // 値をコストバーに反映
    void CostFixed()
    {
        AnimalCostTxt.GetComponent<Text>().text = Mathf.Floor(AnimalCostPoint / 100).ToString("F0");
        WeaponCostText.GetComponent<Text>().text = Mathf.Floor(WeaponCostPoint / 100).ToString("F0");
        AnimalRect.sizeDelta = new Vector2(AnimalCostPoint / MaxAnimalCost * MaxAnimalCost, AnimalRect.sizeDelta.y);
        WeaponRect.sizeDelta = new Vector2(WeaponCostPoint / MaxWeaponCost * MaxWeaponCost, WeaponRect.sizeDelta.y);
    }
}
