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



    //  値をコストバーに反映
    public void CostFixed()
    {
        rect.sizeDelta = new Vector2(CostPoint / MaxCost * 500, rect.sizeDelta.y);
    }
}
