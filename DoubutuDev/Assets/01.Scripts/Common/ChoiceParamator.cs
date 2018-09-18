using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceParamator : MonoBehaviour {
    
    public int[,] SelectParamator = new int[3, 4];
    public float ClearTime = 0f;
    public GameObject FavoAnimal;
    //[[動物, 武器1, 武器2, 武器3],[],[]]
    //for(int i = 0; i < 3)
    //AnimalList[i] = SelectParamator[i, 0];

    void Start () {
        DontDestroyOnLoad(gameObject);
	}

    public void SetParamator(int[,] param)
    {
        SelectParamator = param;
    }
}
