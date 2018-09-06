using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnManager : MonoBehaviour {

    private CostScript costScript;

    [SerializeField]
    private GameObject[] units = new GameObject[8];

    void Awake()
    {
        costScript = GameObject.FindObjectOfType<CostScript>();
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
                var obj = Instantiate(units[costScript.GetSelectNumber()], new Vector3(pos.x, pos.y, pos.z), units[costScript.GetSelectNumber()].transform.rotation);
                //  エフェクトの表示
                EffectManager.Instance_Effect.PlayEffect(EffectManager.EffectKind.Magic, obj.transform.position, 1, obj);
                //  効果音再生
                AudioManager.Instance.PlaySe((int)AudioManager.SelistName.AnimalSpawn);
            }
            else
            {
                //  コストが足りない場合画面に表示

                Debug.Log("コストが足りない");
            }
        }
        else
        {
            //  ボタンを選んでないよを表示

            Debug.Log("ボタン選んでないよ");
        }
            
    }

    public void DragGround()
    {

    }

    //  選ばれた動物編成で配列にセット
    public void SetAnimalsPrefab(GameObject[] _animals)
    {
        units = _animals;
    }
}
