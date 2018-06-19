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
    private GameObject prefab;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ClickGround()
    {
        if (costScript.IsSetCostValue())
        {
            if (costScript.IsCreate())
            {
                costScript.ConsumeAnimalCost();
                costScript.ConsumeWeaponCost();

                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                GameObject obj = Instantiate(prefab, new Vector3(pos.x, pos.y, pos.z), prefab.transform.rotation);

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
}
