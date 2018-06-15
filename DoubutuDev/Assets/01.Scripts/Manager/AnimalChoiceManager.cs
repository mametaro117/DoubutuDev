using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalChoiceManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectColumnClick(GameObject obj)
    {
        Debug.Log("SelectListタップ！");
        Debug.Log(obj.name);
    }

    public void AnimalColumnClick(GameObject obj)
    {
        Debug.Log("AnimalListタップ！");
        Debug.Log(obj.name);
    }
}
