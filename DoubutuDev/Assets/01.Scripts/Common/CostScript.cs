using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostScript : MonoBehaviour {

    //  コストの最大値
    private const float MaxAnimalCost = 500;
    private const float MaxWaponCost = 500;
    [SerializeField]
    private Text AnimalCostTxt,WeaponCostText;
    [SerializeField]
    private float DefaultAnimalCost = 100;
    [SerializeField]
    private float DefaultWeaponCost = 100;

    private float AnimalCostPoint;
    private float WeaponCostPoint;

    [SerializeField]
    private RectTransform Animalrect;
    [SerializeField]
    private RectTransform Weaponrect;


    void Start () {
        //  コストの初期化
        AnimalCostPoint = DefaultAnimalCost;
        WeaponCostPoint = DefaultWeaponCost;
        //  コンポーネントの参照
        Animalrect = GetComponent<RectTransform>();
        Weaponrect = GetComponent<RectTransform>();
    }
	
	void Update () {
        if(AnimalCostPoint < MaxAnimalCost) AnimalCostPoint += Time.deltaTime * 25;
        CostFixed();
    }

    //  コストを消費する
    public void ConsumeCost(float cost)
    {
        cost = cost * 100;
        //  消費コストが現在のコストより小さければ
        if (cost < CostPoint)
        {
            CostPoint -= cost;
            CostFixed();
        }
        else
        {
            Debug.LogWarning("コストが足りないよ");
        }
    }


    //  値をコストバーに反映
    void CostFixed()
    {
        txt.text = (CostPoint / 100).ToString("F0");
        rect.sizeDelta = new Vector2(CostPoint / MaxAnimalCost * MaxAnimalCost, rect.sizeDelta.y);
    }
}
