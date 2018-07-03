using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButtonScript : MonoBehaviour {

    [SerializeField]
    private CostScript costScript;

    private Button btn;

    [SerializeField]
    private float cost;

    [SerializeField]
    private int SelectNum;

    [SerializeField]
    private GameObject[] WeaponButtons = new GameObject[2];


    // Use this for initialization
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(TapWeaponButton);
    }

    void TapWeaponButton()
    {
        //  武器の選択
        costScript.SetWeaponObj(gameObject);

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

    public int GetSelectNum()
    {
        return SelectNum;
    }
}
