using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButtonScript : MonoBehaviour {

    [SerializeField]
    private CostScript costScript;

    private Button btn;

    public float cost;
    public int SelectNum;

    public GameObject[] WeaponButtons = new GameObject[2];


    // Use this for initialization
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(TapWeaponButton);
    }
    void Update()
    {
        
    }

    void TapWeaponButton()
    {
        //  武器の選択
        costScript.SetWeaponObj(gameObject);
        //  選択状態を可視化するためにImageの表示
        GameObject.FindObjectOfType<AnimalButtonManager>().SelectWeapon(transform.position);
        //  押している状態にボタンを変更
        GetComponent<Button>().interactable = false;
        //  このボタン以外を選択解除する
        for (int i = 0; i < WeaponButtons.Length; i++)
        {
            WeaponButtons[i].GetComponent<Button>().interactable = true;
        }
    }

    public float GetCost()
    {
        return cost;
    }

    //  外からコストを変更するため
    public void SetCost(float costNum)
    {
        cost = costNum;
        gameObject.transform.GetChild(1).GetComponent<DisplayCost>().ApplayCost();
    }

    public int GetSelectNum()
    {
        return SelectNum;
    }
}
