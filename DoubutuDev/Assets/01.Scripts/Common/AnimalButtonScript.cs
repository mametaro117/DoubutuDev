using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalButtonScript : MonoBehaviour {

    [SerializeField]
    private CostScript costScript;
    
    private Button btn;

    [SerializeField]
    private float cost;


	// Use this for initialization
	void Start () {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(TapAnimalButton);
    }

    void TapAnimalButton()
    {
        costScript.SetAnimalCost(cost);
        costScript.DeleteWeaponCost();
    }

    public float GetCost()
    {
        return cost;
    }
}
