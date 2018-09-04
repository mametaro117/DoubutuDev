using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCost : MonoBehaviour {

    [SerializeField]
	private GameObject ParentObj;

	public void ApplayCost(){
        if (ParentObj.GetComponent<WeaponButtonScript>() != null)
		{
            //  武器だったらWeaponButtonScriptを参照
            float costnum = ParentObj.GetComponent<WeaponButtonScript>().GetCost();
            transform.GetChild (0).GetComponent<Text>().text = costnum.ToString();
        }
		else if(ParentObj.GetComponent<AnimalButtonScript>() != null)
		{
            //  動物だったらAnimalButtonScriptを参照
            float costnum = ParentObj.GetComponent<AnimalButtonScript> ().GetCost();
            transform.GetChild (0).GetComponent<Text>().text = costnum.ToString();
        }
	}
}
