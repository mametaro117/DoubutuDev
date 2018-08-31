using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimalButtonManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] AnimalButtons = new GameObject[3];
    private ChoiceParamator choiceParamator;


    void Start () {
		
	}
	
	void Update () {
		
	}

    public void SetAnimalStatus(UnitTable table)
    {
        choiceParamator = GameObject.FindObjectOfType<ChoiceParamator>();
        for (int i = 0; i < 3; i++)
        {
            AnimalButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = table.AnimalTable[choiceParamator.SelectParamator[i, 0]].AnimalImage;
            AnimalButtons[i].GetComponent<AnimalButtonScript>().SetCost(table.AnimalTable[choiceParamator.SelectParamator[i, 0]].AnimalCost);
        }
    }
}
