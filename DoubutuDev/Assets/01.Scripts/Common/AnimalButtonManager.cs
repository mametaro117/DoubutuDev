using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimalButtonManager : MonoBehaviour {

    [SerializeField]
    private UnitTable unitTable;
    [SerializeField]
    private GameObject[] AnimalButtons = new GameObject[3];
    [SerializeField]
    private GameObject[] WeaponButtons = new GameObject[3];
    private ChoiceParamator choiceParamator;

    void Start () {
		
	}
	
	void Update () {
		
	}

    //  バトルマネージャーから呼ばれて動物セレクト画面の編成を反映
    public void SetAnimalStatus()
    {
        //  編成情報を引っ張ってくる
        choiceParamator = GameObject.FindObjectOfType<ChoiceParamator>();
        //  編成情報がなかったら作成
        if (choiceParamator == null)
        {
            Debug.Log("<color=red>"+ "入ってる"+ "</color>");
            gameObject.AddComponent<ChoiceParamator>();
            choiceParamator = GetComponent<ChoiceParamator>();
            choiceParamator.SelectParamator[0, 0] = 0;
            choiceParamator.SelectParamator[1, 0] = 1;
            choiceParamator.SelectParamator[2, 0] = 2;

        }
        //  編成情報を動物ボタンに反映
        for (int i = 0; i < 3; i++)
        {
            //  動物画像の反映
            AnimalButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = unitTable.AnimalTable[choiceParamator.SelectParamator[i, 0]].AnimalImage;
            //  動物のコスト設定
            AnimalButtons[i].GetComponent<AnimalButtonScript>().SetCost(unitTable.AnimalTable[choiceParamator.SelectParamator[i, 0]].AnimalCost);
        }
    }

    //  押された動物ボタンによって武器のボタンの編成が変化
    public void ApplayWeaponButton(int SelectNum)
    {
        for (int i = 0; i < 3; i++)
        {
            //  画像の切り替え
            WeaponButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = unitTable.WeaponTable[choiceParamator.SelectParamator[SelectNum, i + 1]].WeaponImage;
            //  コストの反映
            WeaponButtons[i].GetComponent<WeaponButtonScript>().SetCost(unitTable.WeaponTable[choiceParamator.SelectParamator[SelectNum, i + 1]].WeaponCost);
        }

    }
}
