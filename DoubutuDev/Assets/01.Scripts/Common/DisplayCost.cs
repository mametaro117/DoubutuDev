using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCost : MonoBehaviour {

	private GameObject obj;

	// Use this for initialization
	void Start () {
		obj = transform.parent.gameObject;
		ApplayCost ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ApplayCost(){
		if (obj.GetComponent<WeaponButtonScript>() != null)
		{
			float costnum = obj.GetComponent<WeaponButtonScript>().GetCost();
			transform.GetChild (0).GetComponent<Text> ().text = costnum.ToString ();
		}
		else if(obj.GetComponent<AnimalButtonScript>() != null)
		{
			float costnum = obj.GetComponent<AnimalButtonScript> ().GetCost ();
			transform.GetChild (0).GetComponent<Text> ().text = costnum.ToString ();
		}
	}
}
