using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenechenge : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private bool isPlay = false;

    public void ChengeScene(int scenenum)
    {
        Debug.Log("TAP");
        if (!isPlay)
        {
            if(scenenum == 3)
            {
                AnimalChoiceManager Manager = GetComponent<AnimalChoiceManager>();
                if(Manager.AnimalSetCheck())
                {
                    //SelectAnimalListを渡す
                    //StartCoroutine(ChengeSceneCol(scenenum));
                    Change_Screen();
                }
            }
            else
            {
                StartCoroutine(ChengeSceneCol(scenenum));
            }
        }
    }

    IEnumerator ChengeSceneCol(int scenenum)
    {
        Debug.Log("Chenge中");
        isPlay = true;
        FadeManager.Instance.LoadScene(scenenum, 1f);
        yield return new WaitForSeconds(1f);
        isPlay = false;
        yield break;
    }

    public void Select_Weapon(GameObject obj)
    {
        int objnum = int.Parse(obj.name.Substring(obj.name.Length - 1));
        Debug.Log(objnum);
        Equip_Change();
    }
    public void Click_Back_Bottun()
    {

    }

    private void Change_Screen()
    {
        GameObject Weapon_List = GameObject.Find("Weapon_List");
        var RectTransform = Weapon_List.GetComponent<RectTransform>();
        Vector2 ResetPos = new Vector2(0, -150);
        RectTransform.anchoredPosition = ResetPos;
        GameObject.Find("AnimalList").SetActive(false);
        text.text = "ぶきせんたく";
    }

    private void Equip_Change()
    {
        //武器リスト
        GameObject Weapon_Box = GameObject.Find("Weapon_Box");
        var RectTransform_1 = Weapon_Box.GetComponent<RectTransform>();
        Vector2 ResetPos_1 = new Vector2(-420, -50);
        RectTransform_1.anchoredPosition = ResetPos_1;

        //戻るボタン
        GameObject Back_Button = GameObject.Find("Back_Button");
        var RectTransform_2 = Back_Button.GetComponent<RectTransform>();
        Vector2 ResetPos_2 = new Vector2(-500, 280);
        RectTransform_2.anchoredPosition = ResetPos_2;


        GameObject.Find("Text").GetComponent<CanvasGroup>().alpha = 0;
    }
}
