using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceParamator : MonoBehaviour {
<<<<<<< HEAD

    [SerializeField]
    private int[,] SelectParamator = new int[3, 4];
=======
    
    public int[,] SelectParamator = new int[3, 4];
>>>>>>> Hirokawa
	
	void Start () {
        DontDestroyOnLoad(gameObject);
	}

    public void SetParamator(int[,] param)
    {
        SelectParamator = param;
    }
}
