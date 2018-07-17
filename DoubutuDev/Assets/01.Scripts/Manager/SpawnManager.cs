using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnManager : MonoBehaviour {

    private Camera camera;

    [SerializeField]
    private CostScript costScript;

    [SerializeField]
    private GameObject[] units = new GameObject[3];

    void Awake()
    {
        camera = Camera.main;
    }

    public void ClickGround()
    {
        //  どうぶつボタンと武器ボタンが押されていれば
        if (costScript.IsSetCostValue())
        {
            //  コストが両方足りていたらTrue
            if (costScript.IsCreate())
            {
                //  コストの消費
                costScript.ConsumeAnimalCost();
                costScript.ConsumeWeaponCost();
                //  生成する場所の取得
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                //  インスタンス生成
                GameObject obj = Instantiate(units[costScript.GetSelectNumber()], new Vector3(pos.x, pos.y, pos.z), units[costScript.GetSelectNumber()].transform.rotation);
            }
            else
                Debug.Log("コストが足りない");
        }
        else
            Debug.Log("ボタン選んでないよ");        
    }

    public void DragGround()
    {

    }
}
