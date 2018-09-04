using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalButtonScript : MonoBehaviour {

    [SerializeField]
    private CostScript costScript;
    [SerializeField]
    private float cost;
    [SerializeField]
    private int SelectNum;
    private Color disablecolor = new Color(0.5f,0.5f,0.5f,1);

    [SerializeField]
    private GameObject[] AnimalButtons = new GameObject[2];
    [SerializeField]
    private GameObject[] WeaponButtons = new GameObject[3];
    private Button btn;

    // Use this for initialization
    void Start () {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(TapAnimalButton);
    }

    void Update()
    {
        if (cost*100 <= costScript.GetAnimalCost())
        {
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = disablecolor;
        }
    }

    void TapAnimalButton()
    {
        //  動物の選択
        costScript.SetAnimalObj(gameObject);
        //  動物ボタンが押されたら武器の画像やコストを表示する
        GameObject.FindObjectOfType<AnimalButtonManager>().ApplayWeaponButton(SelectNum);
        //  選択状態を可視化するためにImageの表示
        GameObject.FindObjectOfType<AnimalButtonManager>().SelectAnimal(transform.position);
        //  押している状態にボタンを変更
        GetComponent<Button>().interactable = false;
        //  このボタン以外を選択解除する
        for (int i = 0; i < AnimalButtons.Length; i++)
        {
            AnimalButtons[i].GetComponent<Button>().interactable = true;
        }
        //  武器の選択を解除
        costScript.DeleteWeaponCost();
        //  武器ボタンの選択解除する
        for (int i = 0; i < WeaponButtons.Length; i++)
        {
            WeaponButtons[i].GetComponent<Button>().interactable = true;
        }
    }

    public float GetCost()
    {
        return cost;
    }

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
