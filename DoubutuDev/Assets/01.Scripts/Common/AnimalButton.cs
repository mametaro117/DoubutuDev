﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalButton : MonoBehaviour {

    [SerializeField]
    private CostScript costScript;
    private Button btn;

    [SerializeField]
    private float cost;


	// Use this for initialization
	void Start () {
        btn = GetComponent<Button>();
        Test();
    }
	
	// Update is called once per frame
	void Update () {

	}

    void Test()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(TapAnimalButton);
    }

    void TapAnimalButton()
    {
        costScript.ConsumeCost(cost);
    }

}