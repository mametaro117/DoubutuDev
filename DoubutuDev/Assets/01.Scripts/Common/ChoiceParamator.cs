using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceParamator : MonoBehaviour {

    [SerializeField]
    private int[,] SelectParamator = new int[3, 4];
	
	void Start () {
        DontDestroyOnLoad(gameObject);
	}

    public void SetParamator(int[,] param)
    {
        SelectParamator = param;
    }
}
