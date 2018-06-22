using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private CostScript costScript;

    [SerializeField]
    private GameObject ColObj;

    [SerializeField]
    private GameObject[] units = new GameObject[3];

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            BattleManager.i++;
        }
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
                GameObject obj = Instantiate(units[costScript.GetSelectNum()], new Vector3(pos.x, pos.y, pos.z), units[costScript.GetSelectNum()].transform.rotation);

                GameObject col = Instantiate(ColObj, new Vector3(obj.transform.position.x, obj.transform.position.y, 0), Quaternion.identity);
                //  生成したobjにステータスを設定
                //obj.GetComponent<Totalstatus>().SetStatus(10, 3, 1, false);
                //Debug.Log(pos);
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
