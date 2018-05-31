﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private CostScript costScript;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private GameObject[] units = new GameObject[3];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

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
                GameObject obj = Instantiate(prefab, new Vector3(pos.x, pos.y, pos.z), prefab.transform.rotation);
                //  生成したobjにステータスをアタッチ
                obj.AddComponent<Totalstatus>();
                obj.GetComponent<Totalstatus>().SetStatus(10, 3, 1, false);

                Debug.Log(pos);
            }
            else
            {
                Debug.Log("コストが足りない");
            }
        }
        else
        {
            Debug.Log("選んでないよ");
        }        
    }

    public void DragGround()
    {

    }
}
