﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalButtonScript : MonoBehaviour {

    [SerializeField]
    private CostScript costScript;
    [SerializeField]
    private SpawnManager Manager;
    private Button btn;

    [SerializeField]
    private float cost;


	// Use this for initialization
	void Start () {
        btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(TapAnimalButton);
    }

    // Update is called once per frame
    void Update () {

	}


    void AddCostEvent()
    {

    }
    void TapAnimalButton()
    {
        
    }

}
