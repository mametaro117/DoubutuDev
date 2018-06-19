using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCost : MonoBehaviour {

	private GameObject ParentObj;

	// Use this for initialization
	void Start () {
		ParentObj = transform.parent.gameObject;
		ApplayCost ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ApplayCost(){
		if (ParentObj.GetComponent<WeaponButtonScript>() != null)
		{
            //  武器だったらWeaponButtonScriptを参照
			float costnum = ParentObj.GetComponent<WeaponButtonScript>().GetCost();
			transform.GetChild (0).GetComponent<Text> ().text = costnum.ToString ();
		}
		else if(ParentObj.GetComponent<AnimalButtonScript>() != null)
		{
            //  動物だったらAnimalButtonScriptを参照
            float costnum = ParentObj.GetComponent<AnimalButtonScript> ().GetCost ();
			transform.GetChild (0).GetComponent<Text> ().text = costnum.ToString ();
		}
	}
}
