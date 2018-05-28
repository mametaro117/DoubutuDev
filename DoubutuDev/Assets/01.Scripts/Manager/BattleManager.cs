using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    private static List<GameObject> AnimalList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void AddAnimals(GameObject obj)
    {
        AnimalList.Add(obj);
        Debug.Log(AnimalList.Count);
    }
}
