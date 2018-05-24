using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostScript : MonoBehaviour {

    //  コストの最大値
    private const float MaxCost = 500;
    [SerializeField]
    private float DefaultCost = 100;
    [SerializeField]
    private float CostPoint;

    private RectTransform rect;


	void Start () {
        //  コストの初期化
        CostPoint = DefaultCost;
        //  コンポーネントの参照
        rect = GetComponent<RectTransform>();
	}
	
	void Update () {
        if(CostPoint < MaxCost) CostPoint += Time.deltaTime * 25;
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
        rect.sizeDelta = new Vector2(CostPoint / MaxCost * MaxCost, rect.sizeDelta.y);
    }
}
